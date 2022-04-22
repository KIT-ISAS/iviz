#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Text;
using Iviz.Tools;

namespace Iviz.Core
{
    public static class Crc32Calculator
    {
        public const uint DefaultSeed = 0xffffffffu;

        static uint[]? table;
        static uint[] Table => table ??= InitializeTable();

        static uint[] InitializeTable()
        {
            const uint defaultPolynomial = 0xedb88320u;

            uint[] createTable = new uint[256];
            foreach (int i in ..256)
            {
                uint entry = (uint)i;
                foreach (int _ in ..8)
                {
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ defaultPolynomial;
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

        public static unsafe uint Compute<T>(in T value, uint startHash = DefaultSeed) where T : unmanaged
        {
            fixed (T* ptr = &value)
            {
                return Compute(ref *(byte*)ptr, sizeof(T), startHash);
            }
        }

        public static uint Compute(in BuilderPool.BuilderRent value, uint startHash = DefaultSeed)
        {
            return value.Length == value.Chunk.Length
                ? Compute(value.Chunk.Span, startHash)
                : Compute((StringBuilder)value);
        }

        public static uint Compute<T>(T[] array, uint startHash = DefaultSeed) where T : unmanaged
        {
            int length = array.Length;
            return length == 0
                ? startHash
                : Compute(ref Unsafe.As<T, byte>(ref array[0]), length * Unsafe.SizeOf<T>(), startHash);
        }

        public static uint Compute(string array, uint startHash = DefaultSeed)
        {
            return Compute(array.AsSpan(), startHash);
        }

        static uint Compute(ReadOnlySpan<char> array, uint startHash = DefaultSeed)
        {
            int length = array.Length;
            return length == 0
                ? startHash
                : Compute(ref Unsafe.As<char, byte>(ref array.GetReference()), length * sizeof(char), startHash);
        }

        public static uint Compute(ReadOnlySpan<sbyte> array, uint startHash = DefaultSeed)
        {
            int length = array.Length;
            return length == 0
                ? startHash
                : Compute(ref Unsafe.As<sbyte, byte>(ref array.GetReference()), length * sizeof(sbyte),
                    startHash);
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

        static uint Compute(ref byte value, int size, uint startHash)
        {
            uint hash = startHash;
            ref uint mTable = ref Table[0];
            for (int i = 0; i < size; i++)
            {
                uint val = value;
                uint index = (val ^ hash) & 0xff;
                hash = (hash >> 8) ^ mTable.Plus((int)index);
                value = ref value.Plus(1);
            }

            return hash;
        }
    }
}