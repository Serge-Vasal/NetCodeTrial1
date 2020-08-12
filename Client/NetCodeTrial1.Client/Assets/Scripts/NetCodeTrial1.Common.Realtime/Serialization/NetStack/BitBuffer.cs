using System.Runtime.CompilerServices;
using i32 = System.Int32;
using u32 = System.UInt32;
using u64 = System.UInt64;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public abstract partial class BitBuffer
    {
        public const i32 DefaultU32Capacity = BitBufferLimits.MtuIeee802Dot3 / 4;

        protected internal u32[] chunks;
        protected i32 totalNumChunks;
        protected i32 totalNumberBits;

        protected internal i32 chunkIndex;
        protected internal i32 scratchUsedBits;

        // last partially read value
        protected internal u64 scratch;

        protected internal u32[] Chunks
        {
            set
            {
                chunks = value;
                totalNumChunks = chunks.Length;
                totalNumberBits = totalNumChunks * 8 * Unsafe.SizeOf<u32>();
            }
        }

        internal BitBuffer()
        {
        }

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public void Reset()
        {
            chunkIndex = 0;
            scratch = 0;
            scratchUsedBits = 0;
        }

        /// <summary>
        /// Call aligns remaining bits to full bytes.
        /// </summary>
        public void Align()
        {
            if (scratchUsedBits != 0)
            {
                chunks[chunkIndex] = (u32)(scratch & 0xFFFFFFFF);
                scratch >>= 32;
                scratchUsedBits -= 32;
                chunkIndex++;
            }
        }
    }
}
