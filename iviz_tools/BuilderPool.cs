using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;

namespace Iviz.Tools;

/// <summary>
/// Pool to reuse <see cref="StringBuilder"/> instances.
/// </summary>
public static class BuilderPool
{
    /// <summary>
    /// Rents a <see cref="StringBuilder"/> wrapped inside a <see cref="BuilderRent"/>.
    /// </summary>
    /// <returns>A wrapper around a string builder. Dispose it to return the builder to the pool.</returns>
    public static BuilderRent Rent()
    {
        return new BuilderRent(0);
    }

    /// <summary>
    /// A wrapper around a <see cref="StringBuilder"/>.
    /// Creating it will rent a string builder from the pool, or spawn a new one if it is empty.
    /// Disposing it returns the enclosed builder to the <see cref="BuilderPool"/>.
    /// Cast the instance to a <see cref="StringBuilder"/> to obtain the enclosed builder. 
    /// </summary>
    public readonly struct BuilderRent : IDisposable
    {
        static ConcurrentQueue<StringBuilder>? pool;

        static ConcurrentQueue<StringBuilder> Pool => pool ??= new ConcurrentQueue<StringBuilder>();

        readonly StringBuilder builder;

        public ReadOnlyMemory<char> Chunk
        {
            get
            {
                var chunk = GetMainChunk();
                return chunk.Length >= builder.Length
                    ? chunk[..builder.Length]
                    : chunk;
            }
        }

        /// Returns the length of the enclosed string builder.
        public int Length
        {
            get => builder.Length;
            set => builder.Length = value;
        }

        internal BuilderRent(int _)
        {
            if (Pool.TryDequeue(out var entry))
            {
                builder = entry;
                return;
            }

            builder = new StringBuilder(65536);
        }

        public void Dispose()
        {
            builder.Clear();
            Pool.Enqueue(builder);
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(string)"/>. 
        /// </summary>
        public BuilderRent Append(string? s)
        {
            builder.Append(s);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(string, int, int)"/>. 
        /// </summary>
        public BuilderRent Append(string? s, int startIndex, int count)
        {
            builder.Append(s, startIndex, count);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(object)"/>. 
        /// </summary>
        public BuilderRent Append(object? o)
        {
            builder.Append(o);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(bool)"/>. 
        /// </summary>
        public BuilderRent Append(bool b)
        {
            builder.Append(b);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(byte)"/>. 
        /// </summary>
        public BuilderRent Append(byte b)
        {
            builder.Append(b);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(short)"/>. 
        /// </summary>
        public BuilderRent Append(short s)
        {
            builder.Append(s);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(int)"/>. 
        /// </summary>
        public BuilderRent Append(int i)
        {
            builder.Append(i);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(long)"/>. 
        /// </summary>
        public BuilderRent Append(long l)
        {
            builder.Append(l);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(sbyte)"/>. 
        /// </summary>
        public BuilderRent Append(sbyte b)
        {
            builder.Append(b);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(ushort)"/>. 
        /// </summary>
        public BuilderRent Append(ushort s)
        {
            builder.Append(s);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(uint)"/>. 
        /// </summary>
        public BuilderRent Append(uint i)
        {
            builder.Append(i);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(ulong)"/>. 
        /// </summary>
        public BuilderRent Append(ulong l)
        {
            builder.Append(l);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(char)"/>. 
        /// </summary>
        public BuilderRent Append(char c)
        {
            builder.Append(c);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(float)"/>. 
        /// </summary>
        public BuilderRent Append(float f)
        {
            builder.Append(f);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(double)"/>. 
        /// </summary>
        public BuilderRent Append(double d)
        {
            builder.Append(d);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.Append(StringBuilder)"/>. 
        /// </summary>
        public BuilderRent Append(StringBuilder s)
        {
            builder.Append(s);
            return this;
        }

        /// <summary>
        /// Calls <see cref="StringBuilder.AppendLine()"/>. 
        /// </summary>
        public BuilderRent AppendLine()
        {
            builder.AppendLine();
            return this;
        }

        /// <summary>
        /// Copies the content of the string builder to a byte <see cref="Rent"/>. 
        /// </summary>
        public Rent AsRent()
        {
            int length = builder.Length;
            var bytes = new Rent(Defaults.UTF8.GetMaxByteCount(length));
            int size;

            var mainChunk = Chunk;
            if (mainChunk.Length >= length)
            {
                size = Defaults.UTF8.GetBytes(mainChunk[..length].Span, bytes);
            }
            else
            {
                // slow path
                using var chars = new Rent<char>(length);
                var array = chars.AsSpan();
                for (int i = 0; i < length; i++)
                {
                    array[i] = builder[i];
                }

                size = Defaults.UTF8.GetBytes(chars, bytes);
            }

            return bytes.Resize(size);
        }

        public override string ToString()
        {
            return builder.ToString();
        }

        public static implicit operator StringBuilder(BuilderRent s)
        {
            return s.builder;
        }

#if NETSTANDARD2_1
        [StructLayout(LayoutKind.Explicit)]
        struct StringBuilderConverter
        {
            [UsedImplicitly]
            class OpenStringBuilder
            {
                [UsedImplicitly]
                public readonly char[]? chunkChars;
            }

            [FieldOffset(0)] public StringBuilder builder;
            [FieldOffset(0)] readonly OpenStringBuilder openBuilder;

            public char[] ExtractChars() => openBuilder.chunkChars ?? Array.Empty<char>();
        }

        ReadOnlyMemory<char> GetMainChunk()
        {
            return new StringBuilderConverter { builder = builder }.ExtractChars();
        }
#else
        ReadOnlyMemory<char> GetMainChunk()
        {
            var e = builder.GetChunks();
            return !e.MoveNext() ? ReadOnlyMemory<char>.Empty : e.Current;
        }
#endif
    }
}