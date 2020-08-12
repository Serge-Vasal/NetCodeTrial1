using NetCodeTrial1.Common.Realtime.Serialization.NetStack;

namespace NetCodeTrial1.Common.Realtime.Serialization
{
    public static class ByteInfoStreamExtension
    {
        public static void WriteManual(this TypedBitOutStream outStream, byte updated)
        {
            outStream.WriteByte(updated);
        }

        public static void WriteManual(this TypedBitOutStream outStream, ushort updated)
        {
            outStream.WriteShort(updated);
        }
    }
}
