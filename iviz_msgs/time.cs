using System;

namespace Iviz.Msgs
{
    public readonly struct time
    {
        public readonly uint secs;
        public readonly uint nsecs;

        public time(uint secs, uint nsecs)
        {
            this.secs = secs;
            this.nsecs = nsecs;
        }

        static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

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
    }

}