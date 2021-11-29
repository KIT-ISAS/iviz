#nullable enable

using System;
using System.Runtime.CompilerServices;
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

        static unsafe uint CalculateHash(uint hash, byte* ptr, int size)
        {
            for (; size != 0; size--)
            {
                uint val = *ptr++;
                hash = (hash >> 8) ^ Table[val ^ hash & 0xff];
            }

            return hash;
        }
        
        public static unsafe uint Compute<T>(T value, uint startHash = DefaultSeed) where T : unmanaged
        {
            T* ptr = &value;
            return CalculateHash(startHash, (byte*) ptr, sizeof(T));
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

        static unsafe uint Compute<T>(ReadOnlySpan<T> array, uint startHash = DefaultSeed) where T : unmanaged
        {
            fixed (T* ptr = array)
            {
                return CalculateHash(startHash, (byte*) ptr, array.Length * sizeof(T));
            }
        }

        public static unsafe uint Compute(string array, uint startHash = DefaultSeed)
        {
            fixed (char* ptr = array)
            {
                return CalculateHash(startHash, (byte*) ptr, array.Length * sizeof(char));
            }
        }

        public static unsafe uint Compute<T>(in Rent<T> array, uint startHash = DefaultSeed) where T : unmanaged
        {
            fixed (T* ptr = array.Array)
            {
                return CalculateHash(startHash, (byte*) ptr, array.Length * sizeof(T));
            }
        }        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Update(uint hash, byte val)    
        {
            return (hash >> 8) ^ Table[val ^ hash & 0xff];
        } 
    }
}