namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public static class AllocationBitBufferExtensions
    {
        public static byte[] ToArray<T>(this BitBufferWriter<T> self) where T : unmanaged, ICompression<BitBufferWriter<T>>
        {
            var data = new byte[self.LengthWritten];
            self.ToSpan(data);
            return data;
        }
    }
}
