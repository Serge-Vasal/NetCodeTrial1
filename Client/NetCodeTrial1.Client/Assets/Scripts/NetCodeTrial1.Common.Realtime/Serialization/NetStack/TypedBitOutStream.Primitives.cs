using u8 = System.Byte;
using u16 = System.UInt16;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public partial class TypedBitOutStream<TNamespace>
    {
        public void WriteByte(u8 value) => Stream.u8(value);

        public void WriteShort(u16 value) => Stream.u16(value, 16);
    }
}
