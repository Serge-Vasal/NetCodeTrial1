using System;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public class TypedBitInStream : TypedBitInStream<object>
    {
        public TypedBitInStream(int capacity)
            : base(capacity)
        {
        }
    }

    public partial class TypedBitInStream<TNamespace>
    {
        public readonly BitBufferReader<SevenBitDecoding> Stream;

        public TypedBitInStream(int capacity)
        {
            Stream = new BitBufferReader<SevenBitDecoding>(capacity);
        }

        public void FromSpan(Span<byte> span)
        {
            Stream.CopyFrom(span);
        }
    }
}
