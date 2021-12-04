#nullable enable

using System;
using System.Runtime.InteropServices;
using System.Text;
using Iviz.Tools;

namespace Iviz.Core
{
    public static class Crc32Calculator
    {
        const uint DefaultPolynomial = 0xedb88320u;
        public const uint DefaultSeed = 0xffffffffu;

        static readonly uint[] Table = InitializeTable();

        static uint[] InitializeTable()
        {
            uint[] createTable = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                uint entry = (uint) i;
                for (int j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ DefaultPolynomial;
                    }
                    else
                    {
                        entry >>= 1;
                    }
                }

                createTable[i] = entry;
            }

            return createTable;
        }

        public static uint Compute<T>(T value, uint startHash = DefaultSeed) where T : unmanaged
        {
            ReadOnlySpan<T> span = stackalloc T[] { value };
            return Compute(MemoryMarshal.AsBytes(span), startHash);
        }

        public static uint Compute(StringBuilder value, uint startHash = DefaultSeed)
        {
            uint hash = startHash;
            for (int i = 0; i < value.Length; i++)
            {
                uint val = value[i]; // indexing is fast as long as there is only one chunk 
                hash = (hash >> 8) ^ Table[val ^ hash & 0xff];
            }

            return hash;
        }
        
        public static uint Compute(in BuilderPool.BuilderRent value, uint startHash = DefaultSeed)
        {
            return value.Length == value.Chunk.Length 
                ? Compute(value.Chunk.Span, startHash) 
                : Compute((StringBuilder) value);
        }        

        public static uint Compute<T>(T[] array, uint startHash = DefaultSeed) where T : unmanaged
        {
            return Compute(new ReadOnlySpan<T>(array), startHash);
        }

        public static uint Compute(string array, uint startHash = DefaultSeed)
        {
            return Compute(array.AsSpan(), startHash);
        }

        public static uint Compute<T>(ReadOnlySpan<T> array, uint startHash = DefaultSeed) where T : unmanaged
        {
            return Compute(MemoryMarshal.AsBytes(array), startHash);
        }

        static uint Compute(ReadOnlySpan<byte> array, uint startHash = DefaultSeed)
        {
            uint hash = startHash;
            foreach (byte b in array)
            {
                uint val = b;
                hash = (hash >> 8) ^ Table[val ^ hash & 0xff];
            }

            return hash;
        }
    }
}