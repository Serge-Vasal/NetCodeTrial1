using NetCodeTrial1.Common.Realtime.Data.Commands;
using NetCodeTrial1.Common.Realtime.Serialization.NetStack;
using System;

namespace NetCodeTrial1.Common.Realtime.Serialization
{
    public class SimulationCommandSerializer
    {
        private const ushort StreamCapacity = 500;

        private readonly TypedBitOutStream outStream = new TypedBitOutStream(StreamCapacity);
        private readonly TypedBitInStream inStream = new TypedBitInStream(StreamCapacity);

        public void AddCommand(SimulationCommand command)
        {

        }

        public byte[] Serialize(SimulationCommand simulationCommand)
        {
            return SerializeGameCommand(simulationCommand.GameCommand);
        }

        public byte[] Deserialize(Span<byte> span)
        {
            inStream.FromSpan(span);

            var byteData = inStream.ReadByte();
            Console.WriteLine($"byteData: {byteData}");

            var shortData = inStream.ReadShort();
            Console.WriteLine($"shortData: {shortData}");

            var shortSecondData = inStream.ReadShort();
            Console.WriteLine($"shortSecondData: {shortSecondData}");

            return new byte[0];
        }

        private byte[] SerializeGameCommand(IGameCommand gameCommand)
        {
            ByteCommand byteCommand = (ByteCommand)gameCommand;
            outStream.WriteManual(155);

            outStream.WriteShort(60000);

            outStream.WriteShort(60000);



            return outStream.ToArray();
        }
    }
}