using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iviz.App
{
    public class Timeline
    {
        class PoseInfo
        {
            public Pose Pose { get; }
            public DateTime Insertion { get; }
            public readonly string Id;

            public PoseInfo(in Pose pose, string id)
            {
                Pose = pose;
                Insertion = DateTime.Now;
                Id = id;
            }

            public override string ToString()
            {
                return Pose.ToString() + " " + Id;
            }
        }

        readonly SortedList<DateTime, PoseInfo> poses = new SortedList<DateTime, PoseInfo>();
        const int MaxSize = 20;

        public void Add(in DateTime t, in Pose p, string id)
        {
            poses[t] = new PoseInfo(p, id);

            if (poses.Count > MaxSize)
            {
                DateTime minKey = poses.Select(x => (x.Value.Insertion, x.Key)).Min().Key;
                poses.Remove(minKey);
            }
        }

        public Pose Get(in DateTime T)
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

                        Pose pA = poses.Values[i].Pose;
                        Pose pB = poses.Values[i + 1].Pose;

                        return pA.Lerp(pB, (float)t);
                    }
                }
            }
            Debug.Log("OUT! " +  (T - LastTime).TotalMilliseconds);
            return Pose.identity; // shouldn't happen
        }

        public void Clear()
        {
            poses.Clear();
        }

        public Pose First => poses.Values[0].Pose;
        public Pose Last => poses.Values[poses.Count - 1].Pose;

        public DateTime FirstTime => poses.Keys[0];
        public DateTime LastTime => poses.Keys[poses.Count - 1];

        public int Count => poses.Count;
        public bool Empty => Count == 0;
    }
}
