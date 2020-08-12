using u8 = System.Byte;
using u16 = System.UInt16;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public partial class TypedBitInStream<TNamespace>
    {
        public u8 ReadByte() => Stream.u8();

        public u16 ReadShort() => Stream.u16();
    }
}
