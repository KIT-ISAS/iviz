using System;
using System.Collections.Concurrent;
using System.Text;

namespace Iviz.Tools
{
    public static class BuilderPool
    {
        public static BuilderRent Rent()
        {
            return new BuilderRent(0);
        }

        public readonly struct BuilderRent : IDisposable
        {
#if NETSTANDARD2_0
            static readonly ConcurrentBag<(StringBuilder builder, char[] chunk)> Pool = new();
            public readonly char[] chunk;
#else
            static readonly ConcurrentBag<(StringBuilder builder, ReadOnlyMemory<char> chunk)> Pool = new();
            readonly ReadOnlyMemory<char> chunk;

            public ReadOnlyMemory<char> Chunk => chunk.Length >= builder.Length
                ? chunk[..builder.Length]
                : chunk;
#endif

            readonly StringBuilder builder;

            public int Length => builder.Length;

            internal BuilderRent(int _)
            {
                if (Pool.TryTake(out var entry))
                {
                    builder = entry.builder;
                    chunk = entry.chunk;
                    return;
                }

                builder = new StringBuilder(65536);
                chunk = builder.GetMainChunk();
            }

            public void Dispose()
            {
                builder.Clear();
                Pool.Add((builder, chunk));
            }

            public BuilderRent Append(string? s)
            {
                builder.Append(s);
                return this;
            }

            public BuilderRent Append(string? s, int startIndex, int count)
            {
                builder.Append(s, startIndex, count);
                return this;
            }

            public BuilderRent Append(object? o)
            {
                builder.Append(o);
                return this;
            }

            public BuilderRent Append(bool b)
            {
                builder.Append(b);
                return this;
            }

            public BuilderRent Append(byte b)
            {
                builder.Append(b);
                return this;
            }

            public BuilderRent Append(short s)
            {
                builder.Append(s);
                return this;
            }

            public BuilderRent Append(int i)
            {
                builder.Append(i);
                return this;
            }

            public BuilderRent Append(long l)
            {
                builder.Append(l);
                return this;
            }

            public BuilderRent Append(sbyte b)
            {
                builder.Append(b);
                return this;
            }

            public BuilderRent Append(ushort s)
            {
                builder.Append(s);
                return this;
            }

            public BuilderRent Append(uint i)
            {
                builder.Append(i);
                return this;
            }

            public BuilderRent Append(ulong l)
            {
                builder.Append(l);
                return this;
            }

            public BuilderRent Append(char c)
            {
                builder.Append(c);
                return this;
            }

            public BuilderRent Append(float f)
            {
                builder.Append(f);
                return this;
            }

            public BuilderRent Append(double d)
            {
                builder.Append(d);
                return this;
            }
            
            public BuilderRent Append(StringBuilder s)
            {
                builder.Append(s);
                return this;
            }
            
            public BuilderRent AppendLine()
            {
                builder.AppendLine();
                return this;
            }

            public Rent<byte> AsRent()
            {
                return builder.AsRent(chunk);
            }

            public override string ToString()
            {
                return builder.ToString();
            }
            
            public static implicit operator StringBuilder(BuilderRent s)
            {
                return s.builder;
            }
        }
    }
}