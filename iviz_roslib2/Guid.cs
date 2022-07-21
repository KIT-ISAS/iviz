using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Iviz.Tools;

namespace Iviz.Roslib2;

[DataContract]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Guid
{
    readonly ulong a;
    readonly ulong b;
    readonly ulong c;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(in Guid other) => a == other.a && b == other.b && c == other.c;

    public override bool Equals(object? obj) => obj is Guid other && Equals(other);

    public override int GetHashCode()
    {
        ulong l = a ^ b ^ c;
        ulong ll = (l >> 32) ^ (l & (uint.MaxValue - 1));
        return (int)ll;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(in Guid left, in Guid right) => left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(in Guid left, in Guid right) => !left.Equals(right);

    public int CompareTo(in Guid other)
    {
        if (a < other.a) return -1;
        if (a > other.a) return 1;
        if (b < other.b) return -1;
        if (b > other.b) return 1;
        if (c < other.c) return -1;
        if (c > other.c) return 1;
        return 0;
    }

    public static bool operator >(in Guid left, in Guid right) => left.CompareTo(right) == 1;

    public static bool operator <(in Guid left, in Guid right) => left.CompareTo(right) == -1;

    public override string ToString()
    {
        using var rent = new Rent<char>(24 + 24 + 23);
        char[] array = rent.Array;

        int o = 0;

        Append((a >> (0 * 8)) & 0xff);

        for (int i = 1; i < 8; i++)
        {
            array[o++] = '.';
            Append((a >> (i * 8)) & 0xff);
        }

        for (int i = 0; i < 8; i++)
        {
            array[o++] = '.';
            Append((b >> (i * 8)) & 0xff);
        }

        if (c != 0)
        {
            for (int i = 0; i < 8; i++)
            {
                array[o++] = '.';
                Append((c >> (i * 8)) & 0xff);
            }
        }

        void Append(ulong j)
        {
            array[o++] = AsChar(j >> 4);
            array[o++] = AsChar(j & 0xf);
        }

        static char AsChar(ulong j) =>
            j < 10
                ? (char)('0' + j)
                : (char)('a' - 10 + j);

        return new string(array, 0, o);
    }

    [DataMember]
    public string Value => ToString();
}