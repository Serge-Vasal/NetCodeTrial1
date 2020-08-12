using i32 = System.Int32;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public class BitBufferOptions
    {
        public static readonly BitBufferOptions Default = new BitBufferOptions(charSpanBitsLength: DefaultCharSpanBitsLength, u8SpanBitsLength: DefaultU8SpanBitsLength);

        public const i32 DefaultU8SpanBitsLength = 9;

        public const i32 DefaultCharSpanBitsLength = 8;

        private readonly i32 u8SpanLengthMax;

        public i32 U8SpanLengthMax => u8SpanLengthMax;

        private readonly i32 charSpanLengthMax;

        public i32 CharSpanLengthMax => charSpanLengthMax;

        private readonly i32 u8SpanBitsLength;

        public i32 U8SpanBitsLength => u8SpanBitsLength;

        private readonly i32 charSpanBitsLength;

        public i32 CharSpanBitsLength => charSpanBitsLength;

        public BitBufferOptions(i32 charSpanBitsLength = DefaultCharSpanBitsLength, i32 u8SpanBitsLength = DefaultU8SpanBitsLength)
        {
            this.u8SpanBitsLength = u8SpanBitsLength;
            u8SpanLengthMax = (1 << u8SpanBitsLength) - 1;
            this.charSpanBitsLength = charSpanBitsLength;
            charSpanLengthMax = (1 << charSpanBitsLength) - 1;
        }
    }
}
