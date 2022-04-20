using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs;

[DataContract(Name = "time")]
[StructLayout(LayoutKind.Sequential)]
public readonly struct time : IEquatable<time>, IComparable<time>
{
    /// <summary>
    /// Time offset to add to all timestamps.
    /// </summary>
    public static TimeSpan GlobalTimeOffset { get; set; }

    [DataMember(Name = "secs")] public readonly uint Secs;
    [DataMember(Name = "nsecs")] public readonly uint Nsecs;

    public time(uint secs, uint nsecs)
    {
        Secs = secs;
        Nsecs = nsecs;
    }

    /// <summary>
    /// Constructs a time from the given DateTime. Does not consider <see cref="GlobalTimeOffset"/>.
    /// </summary>
    public time(in DateTime time)
    {
        TimeSpan diff = time.ToUniversalTime() - TimeConstants.UnixEpoch;
        Secs = (uint)diff.TotalSeconds;
        Nsecs = (uint)(diff.Ticks % 10000000) * 100;
    }

    /// <summary>
    /// Current time with added <see cref="GlobalTimeOffset"/>.
    /// </summary>
    public static time Now() => new(DateTime.Now + GlobalTimeOffset);

    public DateTime ToDateTime()
    {
        return (TimeConstants.UnixEpoch + ToTimeSpan()).ToLocalTime();
    }

    public TimeSpan ToTimeSpan()
    {
        return TimeSpan.FromSeconds(Secs) + TimeSpan.FromTicks(Nsecs / 100);
    }

    public override bool Equals(object? obj)
    {
        return (obj is time d) && (this == d);
    }

    public override int GetHashCode()
    {
        return (Secs, Nsecs).GetHashCode();
    }

    public static bool operator ==(time left, time right)
    {
        return left.Nsecs == right.Nsecs && left.Secs == right.Secs;
    }

    public static bool operator !=(time left, time right)
    {
        return !(left == right);
    }

    public bool Equals(time other)
    {
        return this == other;
    }

    public int CompareTo(time other)
    {
        int secsComparison = Secs.CompareTo(other.Secs);
        return secsComparison != 0 ? secsComparison : Nsecs.CompareTo(other.Nsecs);
    }

    public static bool operator >(time left, time right)
    {
        return left.Secs != right.Secs ? left.Secs > right.Secs : left.Nsecs > right.Nsecs;
    }

    public static bool operator <(time left, time right)
    {
        return left.Secs != right.Secs ? left.Secs < right.Secs : left.Nsecs < right.Nsecs;
    }

    public static bool operator >=(time left, time right)
    {
        return !(left < right);
    }

    public static bool operator <=(time left, time right)
    {
        return !(left > right);
    }

    public time WithSecs(uint secs)
    {
        return new(secs, Nsecs);
    }

    public override string ToString()
    {
        return $"{{\"secs\":{Secs.ToString()},\"nsecs\":{Nsecs.ToString()}}}";
    }
}
    
internal static class TimeConstants
{
    public static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
}