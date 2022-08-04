﻿using System;
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

    static DateTime? unixEpoch;
    static DateTime UnixEpoch => (unixEpoch ??= new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

    [DataMember(Name = "secs")] public readonly int Secs;
    
    [DataMember(Name = "nsecs")] public readonly int Nsecs;

    public time(int secs, int nsecs)
    {
        Secs = secs;
        Nsecs = nsecs;
    }

    /// <summary>
    /// Constructs a time from the given DateTime. Does not consider <see cref="GlobalTimeOffset"/>.
    /// </summary>
    public time(in DateTime time)
    {
        TimeSpan diff = time.ToUniversalTime() - UnixEpoch;
        Secs = (int)diff.TotalSeconds;
        Nsecs = (int)(diff.Ticks % 10000000) * 100;
    }

    /// <summary>
    /// Current time with added <see cref="GlobalTimeOffset"/>.
    /// </summary>
    public static time Now() => new(DateTime.Now + GlobalTimeOffset);

    public DateTime ToDateTime() => (UnixEpoch + ToTimeSpan()).ToLocalTime();

    public TimeSpan ToTimeSpan() => TimeSpan.FromSeconds(Secs) + TimeSpan.FromTicks(Nsecs / 100);

    public override bool Equals(object? obj) => (obj is time d) && (this == d);

    public override int GetHashCode() => HashCode.Combine(Secs, Nsecs);

    public static bool operator ==(time left, time right) => left.Nsecs == right.Nsecs && left.Secs == right.Secs;

    public static bool operator !=(time left, time right) => !(left == right);

    public bool Equals(time other) => this == other;

    public int CompareTo(time other)
    {
        int secsComparison = Secs.CompareTo(other.Secs);
        return secsComparison != 0 ? secsComparison : Nsecs.CompareTo(other.Nsecs);
    }

    public static bool operator >(time left, time right) =>
        left.Secs != right.Secs ? left.Secs > right.Secs : left.Nsecs > right.Nsecs;

    public static bool operator <(time left, time right) =>
        left.Secs != right.Secs ? left.Secs < right.Secs : left.Nsecs < right.Nsecs;

    public static bool operator >=(time left, time right) => !(left < right);

    public static bool operator <=(time left, time right) => !(left > right);

    public time WithSecs(int secs) => new(secs, Nsecs);

    public override string ToString()
    {
        return $"{{\"secs\":{Secs.ToString()},\"nsecs\":{Nsecs.ToString()}}}";
    }
}