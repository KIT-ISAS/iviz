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
            public DateTime Key { get; }
            public DateTime Insertion { get; }
            public readonly string Id;

            public PoseInfo(in DateTime key, in Pose pose, string id)
            {
                Key = key;
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
        readonly SortedList<DateTime, PoseInfo> insertions = new SortedList<DateTime, PoseInfo>();

        const int MaxSize = 50;

        public void Add(in DateTime t, in Pose p, string id)
        {
            var poseInfo = new PoseInfo(t, p, id);
            poses[t] = poseInfo;
            insertions[poseInfo.Insertion] = poseInfo;

            if (poses.Count > MaxSize)
            {
                DateTime minKey = insertions.Values[0].Key;
                poses.Remove(minKey);
                insertions.RemoveAt(0);
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
                for (int i = poses.Count - 2; i >= 0; i--) // most likely to be at the end
                {
                    if (T > poses.Keys[i])
                    {
                        DateTime A = poses.Keys[i];
                        DateTime B = poses.Keys[i + 1];
                        double t = (T - A).TotalMilliseconds / (B - A).TotalMilliseconds;

                        Pose pA = poses.Values[i].Pose;
                        Pose pB = poses.Values[i + 1].Pose;


                        //Debug.Log(i + "/" + poses.Count + "/" + insertions.Count);
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
