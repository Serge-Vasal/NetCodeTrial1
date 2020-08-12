namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public class TypedBitOutStream : TypedBitOutStream<object>
    {
        public TypedBitOutStream(int capacity)
            : base(capacity)
        {
        }
    }

    public partial class TypedBitOutStream<TNamespace>
    {
        public readonly BitBufferWriter<SevenBitEncoding> Stream;

        public TypedBitOutStream(int capacity)
        {
            Stream = new BitBufferWriter<SevenBitEncoding>(capacity);
        }

        public byte[] ToArray()
        {
            var result = Stream.ToArray();
            Stream.Reset();
            return result;
        }
    }
}
