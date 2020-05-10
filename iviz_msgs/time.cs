using System;
using System.Runtime.InteropServices;

namespace Iviz.Msgs
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct time : IEquatable<time>
    {
        public uint secs { get; }
        public uint nsecs { get; }

        public time(uint secs, uint nsecs)
        {
            this.secs = secs;
            this.nsecs = nsecs;
        }

        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public time(in DateTime time)
        {
            TimeSpan diff = time - UnixEpoch;
            secs = (uint)diff.TotalSeconds;
            nsecs = (uint)(diff.Ticks % 10000000) * 100;
        }

        public DateTime ToDateTime()
        {
            return UnixEpoch.AddSeconds(secs).AddTicks(nsecs / 100);
        }

        public override bool Equals(object obj)
        {
            return (obj is time d) ? (this == d) : false;
        }

        public override int GetHashCode()
        {
            return (secs, nsecs).GetHashCode();
        }

        public static bool operator ==(time left, time right)
        {
            return left.nsecs == right.nsecs && left.secs == right.secs;
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