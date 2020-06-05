using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs
{
    [DataContract(Name = "time")]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct time : IEquatable<time>
    {
        [DataMember(Name = "secs")] public uint Secs { get; }
        [DataMember(Name = "nsecs")] public uint Nsecs { get; }

        public time(uint secs, uint nsecs)
        {
            this.Secs = secs;
            this.Nsecs = nsecs;
        }

        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public time(in DateTime time)
        {
            TimeSpan diff = time - UnixEpoch;
            Secs = (uint)diff.TotalSeconds;
            Nsecs = (uint)(diff.Ticks % 10000000) * 100;
        }

        public DateTime ToDateTime()
        {
            return UnixEpoch.AddSeconds(Secs).AddTicks(Nsecs / 100);
        }

        public override bool Equals(object obj)
        {
            return (obj is time d) ? (this == d) : false;
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
    }

}