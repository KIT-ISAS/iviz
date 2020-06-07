using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iviz.App
{
    public class Timeline
    {
        static readonly TimeSpan MaxDistance = TimeSpan.FromSeconds(20);

        readonly SortedList<DateTime, Pose> poses = new SortedList<DateTime, Pose>();

        public void Add(in DateTime t, in Pose p)
        {
            poses[t] = p;

            while (LastTime - FirstTime > MaxDistance)
            {
                poses.RemoveAt(0);
            }
        }

        public Pose Get(DateTime T)
        {
            if (poses.Count == 0)
            {
                return Pose.identity;
            }
            else if (T >= LastTime)
            {
                return Last;
            }
            else if (T <= FirstTime)
            {
                return First;
            }
            else
            {
                for (int i = 0; i < poses.Count - 1; i++)
                {
                    if (T < poses.Keys[i + 1])
                    {
                        DateTime A = poses.Keys[i];
                        DateTime B = poses.Keys[i + 1];
                        double t = (T - A).TotalMilliseconds / (B - A).TotalMilliseconds;

                        Pose pA = poses.Values[i];
                        Pose pB = poses.Values[i + 1];
                        return pA.Lerp(pB, (float)t);
                    }
                }
            }
            return Pose.identity; // shouldn't happen
        }

        public Pose First => poses.Values[0];
        public Pose Last => poses.Values[poses.Count - 1];

        public DateTime FirstTime => poses.Keys[0];
        public DateTime LastTime => poses.Keys[poses.Count - 1];

        public int Count => poses.Count;
        public bool Empty => Count == 0;
    }
}
