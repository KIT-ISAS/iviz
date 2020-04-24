using System;

namespace Iviz.Msgs
{
    public readonly struct duration
    {
        public readonly int secs;
        public readonly int nsecs;

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

    }

}