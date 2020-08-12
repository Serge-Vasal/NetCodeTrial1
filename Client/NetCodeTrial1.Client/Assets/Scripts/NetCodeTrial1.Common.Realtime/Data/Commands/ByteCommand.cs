namespace NetCodeTrial1.Common.Realtime.Data.Commands
{
    public class ByteCommand : IGameCommand
    {
        public byte ByteData { get; }

        public ByteCommand(byte byteData)
        {
            ByteData = byteData;
        }

        public override string ToString() => $"{nameof(ByteCommand)}{ByteData}";
    }
}
