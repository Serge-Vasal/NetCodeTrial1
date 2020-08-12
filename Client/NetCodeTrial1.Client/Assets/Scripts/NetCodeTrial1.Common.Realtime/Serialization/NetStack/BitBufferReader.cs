using i32 = System.Int32;
using u32 = System.UInt32;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public partial class BitBufferReader<T> : BitBuffer
    {
        private BitBufferOptions config;

        public BitBufferReader(i32 capacity = DefaultU32Capacity, BitBufferOptions config = default)
            :this(new u32[capacity], config)
        {
        }

        public BitBufferReader(u32[] buffer, BitBufferOptions config = default)
        {
            this.config = config == null ? BitBufferOptions.Default : config;

            Chunks = buffer;
            Reset();
        }
    }
}
