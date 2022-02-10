using System;
using System.Collections.Generic;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Controllers.TF
{
    public sealed class Timeline
    {
        readonly struct PoseInfo : IEquatable<PoseInfo>
        {
            public TimeSpan Timestamp { get; }
            public Pose Pose { get; }

            public PoseInfo(in TimeSpan timestamp, in Pose pose)
            {
                Timestamp = timestamp;
                Pose = pose;
            }

            public override string ToString()
            {
                return Pose.ToString();
            }

            public bool Equals(PoseInfo other)
            {
                return Timestamp.Equals(other.Timestamp) && Pose.Equals(other.Pose);
            }

            public override bool Equals(object obj)
            {
                return obj is PoseInfo other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Timestamp.GetHashCode() * 397) ^ Pose.GetHashCode();
                }
            }
        }

        int currentPos;
        int lastPos = -1;

        readonly List<PoseInfo> poses = new List<PoseInfo>();
        readonly List<PoseInfo> sortedPoses = new List<PoseInfo>();
        bool needsSorting;

        const int MaxSize = 30;

        public void Add(in TimeSpan t, in Pose p)
        {
            var poseInfo = new PoseInfo(t, p);

            if (lastPos != -1 && poses[lastPos].Timestamp == t) // updating last value
            {
                poses[lastPos] = poseInfo;
            }
            else if (poses.Count == MaxSize)
            {
                poses[currentPos] = poseInfo;
                lastPos = currentPos;
                currentPos++;
                if (currentPos == MaxSize)
                {
                    currentPos = 0;
                }
            }
            else
            {
                poses.Add(poseInfo);
            }

            needsSorting = true;
        }

        public Pose Lookup(in TimeSpan tsNew)
        {
            if (needsSorting)
            {
                sortedPoses.Clear();
                sortedPoses.AddRange(poses);
                sortedPoses.Sort((p1, p2) => p1.Timestamp.CompareTo(p2.Timestamp));
                needsSorting = false;
            }

            int n = sortedPoses.Count;
            if (n == 0)
            {
                return Pose.identity;
            }

            TimeSpan tsLatest = sortedPoses[n - 1].Timestamp;
            if (tsNew >= tsLatest)
            {
                Pose pLatest = sortedPoses[n - 1].Pose;

                if (sortedPoses.Count < 2)
                {
                    return pLatest;
                }
                
                double deltaNew = (tsNew - tsLatest).TotalMilliseconds;
                double deltaLatest = (tsLatest - sortedPoses[n - 2].Timestamp).TotalMilliseconds;

                if (deltaNew > deltaLatest)
                {
                    return pLatest;
                }
                
                double t = deltaNew / deltaLatest;
                Pose pBeforeLatest = sortedPoses[n - 2].Pose;
                Pose pExtrapolated = new Pose(
                    (pLatest.position - pBeforeLatest.position) + pLatest.position,
                    pLatest.rotation * pBeforeLatest.rotation.Inverse() * pLatest.rotation
                );
                return pLatest.Lerp(pExtrapolated, (float) t);

            }

            if (tsNew <= sortedPoses[0].Timestamp)
            {
                return sortedPoses[0].Pose;
            }

            for (int i = n - 2; i >= 0; i--) // most likely to be at the end
            {
                if (tsNew <= sortedPoses[i].Timestamp)
                {
                    continue;
                }

                TimeSpan a = sortedPoses[i].Timestamp;
                TimeSpan b = sortedPoses[i + 1].Timestamp;
                double t = (tsNew - a).TotalMilliseconds / (b - a).TotalMilliseconds;

                Pose pA = sortedPoses[i].Pose;
                Pose pB = sortedPoses[i + 1].Pose;
                //return (t > 0.5f) ? pB : pA;
                return pA.Lerp(pB, (float) t);
            }

            return sortedPoses[0].Pose; // shouldn't happen
        }

        public void Clear()
        {
            poses.Clear();
            currentPos = 0;
            lastPos = -1;
        }

        public int Count => poses.Count;
    }
}