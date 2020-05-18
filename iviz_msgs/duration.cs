using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct duration : IEquatable<duration>
    {
        [DataMember] public int Secs { get; }
        [DataMember] public int Nsecs { get; }

        public duration(int secs, int nsecs)
        {
            Secs = secs;
            Nsecs = nsecs;
        }

        public duration(in TimeSpan span)
        {
            Secs = span.Seconds;
            Nsecs = (int)(span.Ticks % 10000000) * 100;
        }

        public TimeSpan ToTimeSpan()
        {
            return TimeSpan.FromSeconds(Secs) + TimeSpan.FromTicks(Nsecs / 100);
        }

        public override bool Equals(object obj)
        {
            return (obj is duration d) ? (this == d) : false;
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

        public bool IsZero => Secs == 0 && Nsecs == 0;
    }

}