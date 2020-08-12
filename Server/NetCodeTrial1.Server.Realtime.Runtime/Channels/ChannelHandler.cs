using Microsoft.Extensions.Logging;
using NetCodeTrial1.Server.Realtime.Contracts.Channels;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Runtime.Channels
{
    public class ChannelHandler : IChannelHandler
    {
        private readonly ILogger<ChannelHandler> logger;

        public ChannelHandler(ILogger<ChannelHandler> logger)
        {
            this.logger = logger;
        }

        public void StartProcessingMessages<TMessage>(Channel<TMessage> channel, Action<TMessage> processor, CancellationToken cancellationToken = default)
        {
            Task.Factory.StartNew(() => StartReadAndProcess(channel, processor, cancellationToken), TaskCreationOptions.LongRunning);
        }

        public void StopProcessingMessages<TMessage>(Channel<TMessage> channel)
        {
            if (channel.Writer.TryComplete())
            {
                logger.LogInformation("Stopping processing messages in {messageType} channel.", typeof(TMessage).Name);
            }
            else
            {
                logger.LogWarning("Attempt to mark the {messageType} channel as completed failed.", typeof(TMessage).Name);
            }
        }

        private async void StartReadAndProcess<TMessage>(
            ChannelReader<TMessage> channelReader,
            Action<TMessage> processor,
            CancellationToken cancellationToken)
        {
            try
            {
                while (await channelReader.WaitToReadAsync(cancellationToken))
                {
                    while (channelReader.TryRead(out var message))
                    {
                        try
                        {
                            processor(message);
                        }
                        catch (Exception e)
                        {
                            logger.LogError(e, "Exception during processing message of type {messageType}", message.GetType().Name);
                        }
                    }
                }

                logger.LogInformation("The reading from {messageType} channel was completed successfully.", typeof(TMessage).Name);
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("The reading from {messageType} channel was canceled.", typeof(TMessage).Name);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Something went wrong when reading from {messageType} channel.", typeof(TMessage).Name);
            }
        }
    }
}
