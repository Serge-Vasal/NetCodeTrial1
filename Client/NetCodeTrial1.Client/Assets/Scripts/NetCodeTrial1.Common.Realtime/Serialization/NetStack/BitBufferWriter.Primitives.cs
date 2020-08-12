using System.Runtime.CompilerServices;
using i32 = System.Int32;
using u16 = System.UInt16;
using u32 = System.UInt32;
using u8 = System.Byte;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public partial class BitBufferWriter<T>
    {
        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public void u8(u8 value) => raw(value, 8);

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public void u16(u16 value, i32 numberOfBits) => u32(value, numberOfBits);

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public void u32(u32 value, i32 numberOfBits) => raw(value, numberOfBits);
    }
}
