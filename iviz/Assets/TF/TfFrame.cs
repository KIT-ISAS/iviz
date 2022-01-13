#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Controllers.TF
{
    public abstract class TfFrame : FrameNode
    {
        readonly SortedDictionary<string, TfFrame> children = new();
        readonly List<FrameNode> listeners = new();
        Pose localPose;

        public SortedDictionary<string, TfFrame>.ValueCollection Children => children.Values;

        public string Id { get; }
        public virtual bool LabelVisible { get; set; }
        public abstract bool ConnectorVisible { get; set; }
        public abstract float FrameSize { get; set; }
        public virtual bool TrailVisible { get; set; }
        
        public override TfFrame? Parent
        {
            get => base.Parent;
            set
            {
                if (!TrySetParent(value))
                {
                    RosLogger.Error(
                        $"{this}: Failed to set '{(value != null ? value.Id : "null")}' as a parent to {Id}");
                }
            }
        }


        /// <summary>
        /// Pose in relation to the origin frame in Unity coordinates
        /// </summary>
        public Pose OriginWorldPose => TfListener.RelativeToOrigin(AbsoluteUnityPose);

        /// <summary>
        /// Pose in relation to the fixed frame in Unity coordinates
        /// </summary>
        public Pose FixedWorldPose => TfListener.RelativeToFixedFrame(AbsoluteUnityPose);

        /// <summary>
        /// Pose in relation to the Unity origin in Unity coordinates
        /// </summary>
        public Pose AbsoluteUnityPose => Transform.AsPose();

        bool HasNoListeners => listeners.Count == 0;

        bool IsChildless => children.Count == 0;

        //public string? LastCallerId { get; private set; }

        protected TfFrame(string id)
        {
            Id = id;
            Name = "{" + Id + "}";
        }

        public void AddListener(FrameNode frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            if (!listeners.Contains(frame))
            {
                listeners.Add(frame);
            }
        }

        public void RemoveListener(FrameNode frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            if (HasNoListeners)
            {
                return;
            }

            listeners.Remove(frame);
            CheckIfDead();
        }

        void AddChild(TfFrame frame)
        {
            children.Add(frame.Id, frame);
        }

        void RemoveChild(TfFrame frame)
        {
            if (IsChildless)
            {
                return;
            }

            children.Remove(frame.Id);
        }

        void CheckIfDead()
        {
            if (listeners.RemoveAll(listener => listener == null) != 0)
            {
                Debug.LogWarning($"Frame '{Id}' had a listener that was previously destroyed.");
            }

            if (HasNoListeners && IsChildless)
            {
                TfListener.Instance.MarkAsDead(this);
            }
        }

        public virtual bool TrySetParent(TfFrame? parent)
        {
            if (!IsAlive)
            {
                return false;
            }
            
            if (parent == Parent)
            {
                return true;
            }

            if (parent == null)
            {
                Parent?.RemoveChild(this);
                base.Parent = null;
                return true;
            }

            if (IsChildOf(parent, this))
            {
                parent.CheckIfDead();
                return false;
            }

            Parent?.RemoveChild(this);
            base.Parent = parent;
            parent.AddChild(this);

            return true;
        }

        static bool IsChildOf(TfFrame? maybeChild, TfFrame frame)
        {
            while (true)
            {
                if (maybeChild is null)
                {
                    return false;
                }

                if (maybeChild == frame)
                {
                    return true;
                }

                maybeChild = maybeChild.Parent;
            }
        }

        public bool SetLocalPose(in Pose newPose, string? callerId = null)
        {
            if (localPose.EqualsApprox(newPose))
            {
                return false;
            }

            localPose = newPose;
            Transform.SetLocalPose(newPose);
            return true;
        }
        
        public abstract void ForceInvisible();

        public abstract void Highlight();
    }
}