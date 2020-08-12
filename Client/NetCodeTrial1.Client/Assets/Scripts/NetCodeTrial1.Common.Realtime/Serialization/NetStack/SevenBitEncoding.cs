using System.Runtime.CompilerServices;
using i32 = System.Int32;
using u32 = System.UInt32;

namespace NetCodeTrial1.Common.Realtime.Serialization.NetStack
{
    public struct SevenBitEncoding : ICompression<BitBufferWriter<SevenBitEncoding>>
    {
        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public void i32(BitBufferWriter<SevenBitEncoding> b, i32 value)
        {
            // have tried to have only encode, with no i32 method, 
            // but double layer of constrained generics does not propagate on .NET Core and leads to loss of performance
            u32(b, encode(value));
        }

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public void u32(BitBufferWriter<SevenBitEncoding> b, u32 value)
        // trying to use generic method or class with call for inline and optimize with only interface constraint losses great of performance
        //=> BitOptsExtensions<BitBufferWriter<SevenBitEncoding>>.u32(b,value);
        {
            // TODO: how to use CPU parallelism here ? unrol loop? couple of temporal variables? 
            // TODO: mere 8 and 8 into one 16? write special handling code for 8 and 16 coming from outside?
            do
            {
                var buffer = value & 0b0111_1111u;
                value >>= 7;
                if (value > 0)
                    buffer |= 0b1000_0000u;
                b.raw(buffer, 8);
            }
            while (value > 0);
        }

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public u32 encode(i32 value) => BitOptsExtensions.ZigZag(value);

        [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
        public void i32(BitBufferWriter<SevenBitEncoding> b, i32 value, i32 numberOfBits) =>
            b.raw(encode(value), numberOfBits);
    }
}
