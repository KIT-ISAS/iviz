#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Controllers.TF
{
    public abstract class TfFrame : FrameNode
    {
        [SerializeField] string id = "";

        readonly SortedDictionary<string, TfFrame> children = new();
        readonly List<FrameNode> listeners = new();

        Pose localPose;

        public SortedDictionary<string, TfFrame>.ValueCollection Children => children.Values;

        public virtual string Id
        {
            get => id;
            protected set => id = value ?? throw new ArgumentNullException(nameof(value));
        }

        public virtual bool ForceInvisible { get; set; }
        public virtual bool LabelVisible { get; set; }
        public abstract bool ConnectorVisible { get; set; }
        public abstract float FrameSize { get; set; }
        public virtual bool TrailVisible { get; set; }
        public bool ParentCanChange { get; set; } = true;

        public override TfFrame? Parent
        {
            get => base.Parent;
            set
            {
                if (!TrySetParent(value))
                {
                    RosLogger.Error($"{this}: Failed to set '{(value != null ? value.Id : "null")}' as a parent to {Id}");
                }
            }
        }

        /// <summary>
        /// Pose in relation to the ROS origin in Unity coordinates
        /// </summary>
        public Pose OriginWorldPose => TfListener.RelativeToOrigin(AbsoluteUnityPose);

        public Pose FixedWorldPose => TfListener.RelativeToFixedFrame(AbsoluteUnityPose);

        /// <summary>
        /// Pose in relation to the Unity origin in Unity coordinates
        /// </summary>
        public Pose AbsoluteUnityPose => Transform.AsPose();

        bool HasNoListeners => listeners.Count == 0;

        bool IsChildless => children.Count == 0;
        
        public string? LastCallerId { get; private set; }

        public void Setup(string newId)
        {
            Id = newId;
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
                Debug.LogWarning($"Frame '{id}' had a listener that was previously destroyed.");
            }

            if (HasNoListeners && IsChildless)
            {
                TfListener.Instance.MarkAsDead(this);
            }
        }

        public virtual bool TrySetParent(TfFrame? newParent)
        {
            if (!ParentCanChange)
            {
                return false;
            }
            
            if (!IsAlive)
            {
                return false; // destroying!
            }

            TfFrame? oldParent = Parent;

            if (newParent is null)
            {
                if (oldParent is not null)
                {
                    oldParent.RemoveChild(this);
                }

                base.Parent = null;
            }
            else
            {
                if (IsChildOf(newParent, this))
                {
                    newParent.CheckIfDead();
                    return false;
                }

                if (newParent == oldParent)
                {
                    return true;
                }

                if (oldParent is not null)
                {
                    oldParent.RemoveChild(this);
                }

                base.Parent = newParent;
                newParent.AddChild(this);
            }

            return true;
        }

        static bool IsChildOf(TfFrame? maybeChild, Object frame)
        {
            int frameId = frame.GetInstanceID();

            while (true)
            {
                if (maybeChild is null)
                {
                    return false;
                }

                if (maybeChild.GetInstanceID() == frameId)
                {
                    return true;
                }

                maybeChild = maybeChild.Parent;
            }
        }

        public void SetPose(in Pose newPose, string? callerId = null)
        {
            LastCallerId = callerId;
            
            if (localPose.EqualsApprox(newPose))
            {
                return;
            }

            localPose = newPose;
            Transform.SetLocalPose(newPose);
        }

        public abstract void Highlight();
    }
}