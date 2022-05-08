using System;
using System.Collections.Concurrent;
using System.Text;

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
        static ConcurrentQueue<(StringBuilder builder, ReadOnlyMemory<char> chunk)>? pool;

        static ConcurrentQueue<(StringBuilder builder, ReadOnlyMemory<char> chunk)> Pool =>
            pool ??= new ConcurrentQueue<(StringBuilder builder, ReadOnlyMemory<char> chunk)>();

        readonly StringBuilder builder;
        readonly ReadOnlyMemory<char> chunk;

        public ReadOnlyMemory<char> Chunk => chunk.Length >= builder.Length
            ? chunk[..builder.Length]
            : chunk;

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
            Pool.Enqueue((builder, chunk));
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
        /// Copies the content of the string builder to a byte <see cref="Rent{T}"/>. 
        /// </summary>
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