using System;
using System.Collections;
using System.Collections.Generic;
using Iviz.Msgs.SensorMsgs;

namespace Iviz.Msgs
{
    /// <summary>
    /// A wrapper around a <see cref="UniqueRef{T}"/> of bytes that represents a string.
    /// It contains functions to cast this ref to a string or vice-versa (assuming a UTF-8 encoding).
    /// It is assumed that the contents of <see cref="Array"/> are read-only and will not be changed
    /// after construction. If a StringRef is cast to a string, the resulting string will be cached
    /// and reused in future casts. 
    /// </summary>
    public sealed class StringRef : IDisposable, IReadOnlyList<byte>
    {
        public static readonly StringRef Empty = new();

        readonly UniqueRef<byte> uRef;
        string? cachedString;

        public int Length => uRef.Length;
        public byte[] Array => uRef.Array;
        internal UniqueRef<byte> Ref => uRef;
        public ArraySegment<byte> Segment => uRef.Segment;

        StringRef()
        {
            uRef = UniqueRef<byte>.Empty;
        }

        // hidden
        StringRef(string s)
        {
            throw new NotImplementedException();
        }

        StringRef(StringRef other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            uRef = other.uRef.Release();
            cachedString = other.cachedString;
            other.cachedString = null;
        }

        internal StringRef(uint count)
        {
            uRef = new UniqueRef<byte>(count);
        }

        public byte this[int index]
        {
            get => uRef[index];
            set => uRef[index] = value;
        }

        int IReadOnlyCollection<byte>.Count => Length;

        public void Dispose()
        {
            uRef.Dispose();
            cachedString = null;
        }

        IEnumerator<byte> IEnumerable<byte>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public RentEnumerator<byte> GetEnumerator() => uRef.GetEnumerator();

        public StringRef Release() => new(this);
        
        public static implicit operator StringRef(string s)
        {
            if (s == null || s.Length == 0)
            {
                return Empty;
            }

            var array = new StringRef((uint) BuiltIns.UTF8.GetByteCount(s));
            BuiltIns.UTF8.GetBytes(s, 0, s.Length, array.Array, 0);
            array.cachedString = s;
            return array;
        }

        public static implicit operator string(StringRef s)
        {
            return s?.ToString() ?? "";
        }

        public override string ToString()
        {
            if (cachedString != null)
            {
                return cachedString;
            }

            cachedString = Length == 0 ? "" : BuiltIns.UTF8.GetString(Array, 0, Length);
            return cachedString;
        }
    }
}