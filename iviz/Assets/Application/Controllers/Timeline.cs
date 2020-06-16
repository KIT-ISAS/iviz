using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iviz.App
{
    public sealed class Timeline
    {
        readonly struct PoseInfo
        {
            public TimeSpan Key { get; }
            public Pose Pose { get; }

            public PoseInfo(in TimeSpan key, in Pose pose)
            {
                Key = key;
                Pose = pose;
            }

            public override string ToString()
            {
                return Pose.ToString();
            }
        }

        int start;

        readonly List<PoseInfo> poses = new List<PoseInfo>();

        const int MaxSize = 30;

        public void Add(in TimeSpan t, in Pose p)
        {
            var poseInfo = new PoseInfo(t, p);

            if (poses.Count == MaxSize)
            {
                poses[start] = poseInfo;
                start++;
                if (start == MaxSize)
                {
                    start = 0;
                }
            }
            else
            {
                poses.Add(poseInfo);
            }
        }

        public Pose Get(in TimeSpan T)
        {
            var poses = new List<PoseInfo>(this.poses);
            poses.Sort((p1, p2) => p1.Key.CompareTo(p2.Key));

            int n = poses.Count;
            if (n == 0)
            {
                return Pose.identity;
            }
            else if (T >= poses[n - 1].Key)
            {
                return poses[n - 1].Pose;
            }
            else if (T <= poses[0].Key)
            {
                return poses[0].Pose;
            }
            else
            {
                for (int i = n - 2; i >= 0; i--) // most likely to be at the end
                {
                    if (T > poses[i].Key)
                    {
                        TimeSpan A = poses[i].Key;
                        TimeSpan B = poses[i + 1].Key;
                        double t = (T - A).TotalMilliseconds / (B - A).TotalMilliseconds;

                        Pose pA = poses[i].Pose;
                        Pose pB = poses[i + 1].Pose;


                        //Debug.Log(i + "/" + poses.Count + "/" + insertions.Count);
                        return pA.Lerp(pB, (float)t);
                    }
                }
            }
            Debug.Log("OUT!");
            return Pose.identity; // shouldn't happen
        }

        public void Clear()
        {
            poses.Clear();
            start = 0;
        }

        public int Count => poses.Count;
        public bool Empty => Count == 0;
    }
}
