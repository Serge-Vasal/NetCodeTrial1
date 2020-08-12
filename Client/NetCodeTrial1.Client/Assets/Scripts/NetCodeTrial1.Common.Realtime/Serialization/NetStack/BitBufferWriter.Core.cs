using System;
using System.Runtime.CompilerServices;
using i32 = System.Int32;
using u32 = System.UInt32;
using u64 = System.UInt64;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public partial class BitBufferWriter<T> : IRawWriter
        where T : unmanaged, ICompression<BitBufferWriter<T>>
    {
        /// <summary>
        /// Count of written bytes.
        /// </summary>
        public i32 LengthWritten => ((BitsWritten - 1) >> 3) + 1;

        /// <summary>
        /// Gets total count of used bits since buffer start.
        /// </summary>
        public i32 BitsWritten
        {
            get
            {
                var indexInBits = chunkIndex * 32;
                var over = scratchUsedBits != 0 ? 1 : 0; // TODO: speed up with bit hacking
                return indexInBits + over * Math.Abs(scratchUsedBits);
            }
        }

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public void raw(uint value, int numberOfBits)
        {
            internalRaw(value, numberOfBits);
        }

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        private void internalRaw(u32 value, i32 numberOfBits)
        {
            value &= (u32)((1ul << numberOfBits) - 1);

            scratch |= ((u64)value) << scratchUsedBits;

            scratchUsedBits += numberOfBits;

            if (scratchUsedBits >= 32)
            {
                chunks[chunkIndex] = (u32)(scratch);

                var s = Convert.ToString(chunks[chunkIndex], 2);

                scratch >>= 32;
                scratchUsedBits -= 32;
                chunkIndex++;
            }
        }
    }
}
