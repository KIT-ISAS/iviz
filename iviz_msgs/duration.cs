using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs;

[DataContract(Name = "duration")]
[StructLayout(LayoutKind.Sequential)]
public readonly struct duration : IEquatable<duration>, IComparable<duration>
{
    [DataMember(Name = "secs")] public readonly int Secs;
    [DataMember(Name = "nsecs")] public readonly int Nsecs;

    public duration(int secs, int nsecs)
    {
        Secs = secs;
        Nsecs = nsecs;
    }

    public duration(in TimeSpan span)
    {
        Secs = span.Seconds;
        Nsecs = (int) (span.Ticks % 10000000) * 100;
    }

    public TimeSpan ToTimeSpan()
    {
        return TimeSpan.FromSeconds(Secs) + TimeSpan.FromTicks(Nsecs / 100);
    }

    public override bool Equals(object? obj)
    {
        return (obj is duration d) && (this == d);
    }

    public override int GetHashCode()
    {
        return (Secs, Nsecs).GetHashCode();
    }

    public static bool operator ==(duration left, duration right)
    {
        return left.Nsecs == right.Nsecs && left.Secs == right.Secs;
    }

    public static bool operator !=(duration left, duration right)
    {
        return !(left == right);
    }

    public bool Equals(duration other)
    {
        return this == other;
    }

    public static implicit operator duration(in TimeSpan t) => new(t);
        
    public static implicit operator TimeSpan(in duration t) => t.ToTimeSpan();

    public int CompareTo(duration other)
    {
        int secsComparison = Secs.CompareTo(other.Secs);
        return secsComparison != 0 ? secsComparison : Nsecs.CompareTo(other.Nsecs);
    }

    public static bool operator >(duration left, duration right)
    {
        return left.Secs != right.Secs ? left.Secs > right.Secs : left.Nsecs > right.Nsecs;
    }
        
    public static bool operator <(duration left, duration right)
    {
        return left.Secs != right.Secs ? left.Secs < right.Secs : left.Nsecs < right.Nsecs;
    }

    public static bool operator >=(duration left, duration right)
    {
        return !(left < right);
    }
        
    public static bool operator <=(duration left, duration right)
    {
        return !(left > right);
    }

    public override string ToString()
    {
        return $"{{\"secs\":{Secs.ToString()},\"nsecs\":{Nsecs.ToString()}}}";
    }
}