#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Iviz.Tools;

namespace Iviz.Core
{
    public static class Crc32Calculator
    {
        const uint DefaultPolynomial = 0xedb88320u;
        public const uint DefaultSeed = 0xffffffffu;

        static uint[]? table; 
        static uint[] Table => table ??= InitializeTable();

        static uint[] InitializeTable()
        {
            uint[] createTable = new uint[256];
            foreach (int i in ..256)
            {
                uint entry = (uint) i;
                foreach (int _ in ..8)
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
        
        public static uint Compute<T>(in T value, uint startHash = DefaultSeed) where T : unmanaged
        {
            ref T valueRef = ref Unsafe.AsRef(value);
            var span = MemoryMarshal.CreateReadOnlySpan(ref valueRef, 1);
            return Compute(MemoryMarshal.AsBytes(span), startHash);
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

        static uint Compute(StringBuilder value, uint startHash = DefaultSeed)
        {
            // this function is called very rarely. see below the ReadOnlySpan<byte> overload for the most common path.
            uint hash = startHash;
            uint[] mTable = Table;
            for (int i = 0; i < value.Length; i++)
            {
                uint val = value[i]; // indexing is fast as long as there is only one chunk 
                uint index = (val ^ hash) & 0xff;
                hash = (hash >> 8) ^ mTable[index];
            }

            return hash;
        }
        
        static uint Compute(ReadOnlySpan<byte> array, uint startHash = DefaultSeed)
        {
            uint hash = startHash;
            uint[] mTable = Table;
            foreach (byte b in array)
            {
                uint val = b;
                uint index = (val ^ hash) & 0xff;
                hash = (hash >> 8) ^ mTable[index];
            }

            return hash;
        }
    }
}