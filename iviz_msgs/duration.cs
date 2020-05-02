using System;

namespace Iviz.Msgs
{
    public readonly struct duration : IEquatable<duration>
    {
        public int secs { get; }
        public int nsecs { get; }

        public duration(int secs, int nsecs)
        {
            this.secs = secs;
            this.nsecs = nsecs;
        }

        public duration(in TimeSpan span)
        {
            secs = span.Seconds;
            nsecs = (int)(span.Ticks % 10000000) * 100;
        }

        public TimeSpan ToTimeSpan()
        {
            return TimeSpan.FromSeconds(secs) + TimeSpan.FromTicks(nsecs / 100);
        }

        public override bool Equals(object obj)
        {
            return (obj is duration d) ? (this == d) : false;
        }

        public override int GetHashCode()
        {
            return (secs, nsecs).GetHashCode();
        }

        public static bool operator ==(duration left, duration right)
        {
            return left.nsecs == right.nsecs && left.secs == right.secs;
        }

        public static bool operator !=(duration left, duration right)
        {
            return !(left == right);
        }

        public bool Equals(duration other)
        {
            return this == other;
        }
    }

}