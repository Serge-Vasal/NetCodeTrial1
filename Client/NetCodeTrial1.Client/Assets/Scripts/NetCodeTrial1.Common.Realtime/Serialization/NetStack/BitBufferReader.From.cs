using System;
using i32 = System.Int32;
using u8 = System.Byte;
using u32 = System.UInt32;
using System.Runtime.CompilerServices;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public partial class BitBufferReader<T>
    {
        public void CopyFrom(ReadOnlySpan<u8> data)
        {
            var length = data.Length;
            Reset();
            var step = Unsafe.SizeOf<u32>();
            i32 numChunks = (length / step) + 1;

            if (chunks.Length < numChunks)
            {
                Chunks = new u32[numChunks];
            }

            for (var i = 0; i < numChunks; i++)
            {
                i32 dataIdx = i * step;
                u32 chunk = 0;

                if (dataIdx < length)
                    chunk = (u32)data[dataIdx];

                if (dataIdx + 1 < length)
                    chunk = chunk | (u32)data[dataIdx + 1] << 8;

                if (dataIdx + 2 < length)
                    chunk = chunk | (u32)data[dataIdx + 2] << 16;

                if (dataIdx + 3 < length)
                    chunk = chunk | (u32)data[dataIdx + 3] << 24;

                chunks[i] = chunk;
            }
        }
    }
}
