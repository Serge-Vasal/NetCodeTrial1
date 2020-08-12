using System.Runtime.CompilerServices;
using i32 = System.Int32;
using u32 = System.UInt32;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public interface IRawWriter
    {
        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        void raw(u32 value, i32 numberOfBits);
    }
}
