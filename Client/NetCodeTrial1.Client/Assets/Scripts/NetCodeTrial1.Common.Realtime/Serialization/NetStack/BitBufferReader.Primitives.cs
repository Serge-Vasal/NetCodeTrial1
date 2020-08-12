using System.Runtime.CompilerServices;
using u8 = System.Byte;
using u16 = System.UInt16;
using u32 = System.UInt32;
using i32 = System.Int32;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public partial class BitBufferReader<T>
    {
        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public u8 u8() => (u8)raw(8);

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public u16 u16() => (u16)u32(16);

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public u32 u32(i32 numberOfBits) => raw(numberOfBits);
    }
}
