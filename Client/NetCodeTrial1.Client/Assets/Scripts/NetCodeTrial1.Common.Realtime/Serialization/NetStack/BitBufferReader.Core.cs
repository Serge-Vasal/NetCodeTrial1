using System.Runtime.CompilerServices;
using i32 = System.Int32;
using u32 = System.UInt32;
using u64 = System.UInt64;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public partial class BitBufferReader<T> : IRawReader
        where T : unmanaged, IDecompression<BitBufferReader<T>>
    {
        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public u32 raw(i32 numberOfBits)
        {
            if(scratchUsedBits < numberOfBits)
            {
                scratch |= ((u64)(chunks[chunkIndex])) << scratchUsedBits;
                scratchUsedBits += 32;
                chunkIndex++;
            }

            u32 output = (u32)(scratch & ((((u64)1) << numberOfBits) - 1));

            scratch >>= numberOfBits;
            scratchUsedBits -= numberOfBits;

            return output;
        }
    }
}
