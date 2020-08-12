using System.Runtime.CompilerServices;
using i32 = System.Int32;
using u32 = System.UInt32;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public interface ICompression<T> where T : IRawWriter
    {
        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        void u32(T b, u32 value);

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        void i32(T b, i32 value);

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        u32 encode(i32 value);

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        void i32(T b, i32 value, i32 numberOfBits);
    }
}
