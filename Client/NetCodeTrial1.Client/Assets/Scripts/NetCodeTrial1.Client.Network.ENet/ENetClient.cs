using ENet;
using NetCodeTrial1.Client.Realtime.Connection;
using NetCodeTrial1.Client.Realtime.Service;
using System;
using System.Collections.Generic;
using UnityEngine;
using ENetEvent = ENet.Event;
using ENetEventType = ENet.EventType;

namespace NetCodeTrial1.Client.Network.ENet
{
    public class ENetClient : IGameplayConnection
    {
        private const byte ChannelCount = 4;

        public event Action OnDisconnected;
        public event Action OnDisconnectedByServer;

        private readonly ENetClientSettings eNetClientSettings;

        private bool isClientDisconnected;
        private Queue<INetworkData> incomingQueue;

        private Host client;
        private Address address;
        private Peer peer;

        public ConnectionState ConnectionState { get; private set; }

        public string ServerAddress => $"{eNetClientSettings.ServerHostName}:{eNetClientSettings.ServerPort}";

        public ENetClient(ENetClientSettings eNetClientSettings)
        {
            this.eNetClientSettings = eNetClientSettings;

            isClientDisconnected = false;

            incomingQueue = new Queue<INetworkData>();

            Library.Initialize();

            client = new Host();
            address.SetHost(eNetClientSettings.ServerHostName);
            address.Port = eNetClientSettings.ServerPort;
            client.Create();
        }

        public void Connect()
        {
            if (ConnectionState == ConnectionState.Disconnected)
            {
                peer = client.Connect(address, ChannelCount);
                ConnectionState = ConnectionState.Connecting;
            }
        }

        public void Disconnect()
        {
            isClientDisconnected = true;
            peer.Disconnect(0u);
        }

        public void DisconnectNow()
        {
            client.Service(0, out var @event);
            Service(ref @event);
        }

        public INetworkData GetData()
        {
            throw new NotImplementedException();
        }

        public bool HasData()
        {
            throw new NotImplementedException();
        }

        public void SendReliable(byte[] data, int length)
        {
            throw new NotImplementedException();
        }

        public void SendUnreliable(byte[] data, int length)
        {
            throw new NotImplementedException();
        }

        public void ServiceAll()
        {
            throw new NotImplementedException();
        }

        public void ServiceOnce()
        {
            client.Service(0, out var @event);
            Service(ref @event);
        }

        private bool Service(ref ENetEvent @event)
        {
            switch (@event.Type)
            {
                case ENetEventType.None:
                    return false;

                case ENetEventType.Connect:
                    HandleConnect();
                    return true;

                case ENetEventType.Disconnect:
                    HandleDisconnect();
                    return true;

                case ENetEventType.Timeout:
                    HandleTimeout();
                    return true;

                case ENetEventType.Receive:
                    Debug.Log("Packet received from server - Channel ID: " + @event.ChannelID + ", Data length: " + @event.Packet.Length);
                    @event.Packet.Dispose();
                    return true;
            }

            return false;
        }

        private void HandleConnect()
        {
            ConnectionState = ConnectionState.Connected;
        }

        private void HandleDisconnect()
        {
            ConnectionState = ConnectionState.Disconnected;
            if (isClientDisconnected)
            {
                OnDisconnected?.Invoke();
            }
            else
            {
                OnDisconnectedByServer?.Invoke();
            }
        }

        private void HandleTimeout()
        {
            ConnectionState = ConnectionState.Disconnected;
            OnDisconnectedByServer?.Invoke();
        }
    }
}