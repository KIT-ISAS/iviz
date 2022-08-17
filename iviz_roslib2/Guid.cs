using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Roslib2;

/// Global unique identifier.
[DataContract, StructLayout(LayoutKind.Sequential)]
public readonly struct Guid : IEquatable<Guid>, IComparable<Guid>
{
    readonly ulong a;
    readonly ulong b;
    readonly ulong c;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool Equals(in Guid other) => a == other.a && b == other.b /* && c == other.c */;

    public override bool Equals(object? obj) => obj is Guid other && Equals(other);

    public override int GetHashCode()
    {
        ulong l = a ^ b /*^ c*/;
        ulong ll = (l >> 32) ^ l;
        return (int)ll;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(in Guid left, in Guid right) => left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(in Guid left, in Guid right) => !left.Equals(right);

    bool IEquatable<Guid>.Equals(Guid other) => Equals(other);

    int IComparable<Guid>.CompareTo(Guid other) => CompareTo(other);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(in Guid other)
    {
        if (a < other.a) return -1;
        if (a > other.a) return 1;
        if (b < other.b) return -1;
        if (b > other.b) return 1;
        /*
        if (c < other.c) return -1;
        if (c > other.c) return 1;
        */
        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(in Guid left, in Guid right) => left.CompareTo(right) == 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(in Guid left, in Guid right) => left.CompareTo(right) == -1;

    public override string ToString()
    {
        Span<char> array = stackalloc char[24 + 24 + 23]; // 'xx:' times 24 - 1

        int o = 0;

        Append(array, a, 0);

        for (int i = 1; i < 8; i++)
        {
            array[o++] = '.';
            Append(array, a, i);
        }

        for (int i = 0; i < 8; i++)
        {
            array[o++] = '.';
            Append(array, b, i);
        }

        if (c != 0)
        {
            for (int i = 0; i < 8; i++)
            {
                array[o++] = '.';
                Append(array, c, i);
            }
        }
        
        void Append(Span<char> array, ulong k, int i)
        {
            ulong j = (k >> (i * 8)) & 0xff;
            array[o++] = AsChar(j >> 4);
            array[o++] = AsChar(j & 0xf);
        }

        static char AsChar(ulong j) =>
            j < 10
                ? (char)('0' + j)
                : (char)('a' - 10 + j);

        return new string(array[..o]);
    }

    [DataMember]
    public string Value => ToString();
}