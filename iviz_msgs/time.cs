using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs
{
    [DataContract(Name = "time")]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct time : IEquatable<time>, IComparable<time>
    {
        [DataMember(Name = "secs")] public uint Secs { get; }
        [DataMember(Name = "nsecs")] public uint Nsecs { get; }

        public time(uint secs, uint nsecs)
        {
            Secs = secs;
            Nsecs = nsecs;
        }

        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public time(in DateTime time)
        {
            TimeSpan diff = time - UnixEpoch;
            Secs = (uint)diff.TotalSeconds;
            Nsecs = (uint)(diff.Ticks % 10000000) * 100;
        }

        public static time Now()
        {
            return new time(DateTime.UtcNow);
        }
        
        public DateTime ToDateTime()
        {
            return UnixEpoch.AddSeconds(Secs).AddTicks(Nsecs / 100).ToLocalTime();
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
    }

}