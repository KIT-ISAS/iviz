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
        readonly List<PoseInfo> sortedPoses = new List<PoseInfo>();
        bool needsSorting;

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
            needsSorting = true;
        }

        public Pose Get(in TimeSpan ts)
        {
            if (needsSorting)
            {
                sortedPoses.Clear();
                sortedPoses.AddRange(poses);
                sortedPoses.Sort((p1, p2) => p1.Key.CompareTo(p2.Key));
                needsSorting = false;
            }

            int n = sortedPoses.Count;
            if (n == 0)
            {
                return Pose.identity;
            }

            if (ts >= sortedPoses[n - 1].Key)
            {
                return sortedPoses[n - 1].Pose;
            }

            if (ts <= sortedPoses[0].Key)
            {
                return sortedPoses[0].Pose;
            }

            for (int i = n - 2; i >= 0; i--) // most likely to be at the end
            {
                if (ts <= sortedPoses[i].Key)
                {
                    continue;
                }
                TimeSpan a = sortedPoses[i].Key;
                TimeSpan b = sortedPoses[i + 1].Key;
                double t = (ts - a).TotalMilliseconds / (b - a).TotalMilliseconds;

                Pose pA = sortedPoses[i].Pose;
                Pose pB = sortedPoses[i + 1].Pose;


                //Debug.Log(i + "/" + poses.Count + "/" + insertions.Count);
                return pA.Lerp(pB, (float)t);
            }
            //Debug.Log("OUT!");
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
