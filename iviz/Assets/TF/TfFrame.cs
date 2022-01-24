#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Msgs.Tf2Msgs;
using UnityEngine;

namespace Iviz.Controllers.TF
{
    /// <summary>
    /// Class that represents a ROS transform frame as described in a <see cref="TFMessage"/>.
    /// Displays or other nodes that want to attach themselves to a TF frame should spawn a <see cref="FrameNode"/>,
    /// attach themselves to that node, and then attach the node to the TFFrame.
    /// The presence of FrameNodes is used by <see cref="TfListener"/> to keep track of which frames are being used and which
    /// ones can be forgotten if necessary.
    /// </summary>
    public abstract class TfFrame : FrameNode
    {
        readonly SortedDictionary<string, TfFrame> children = new();
        List<FrameNode>? listeners;
        Pose localPose;

        bool HasNoListeners => listeners == null || listeners.Count == 0;
        bool IsChildless => children.Count == 0;

        public string Id { get; }
        public virtual bool LabelVisible { get; set; }
        public abstract bool ConnectorVisible { get; set; }
        public abstract float FrameSize { get; set; }
        public virtual bool TrailVisible { get; set; }
        public abstract bool EnableCollider { set; }

        public SortedDictionary<string, TfFrame>.ValueCollection Children => children.Values;

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


        public override TfFrame? Parent
        {
            get => base.Parent;
            set
            {
                if (!TrySetParent(value))
                {
                    RosLogger.Error($"{this}: Failed to set parent '{value?.Id ?? "null"}'");
                }
            }
        }

        protected TfFrame(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = "{" + id + "}";
        }

        internal void AddListener(FrameNode frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            if (!frame.IsAlive)
            {
                Debug.LogWarning($"{this}: Rejecting listener node '{frame}' that was already disposed.");
                return;
            }

            if (listeners == null)
            {
                listeners = new List<FrameNode> { frame };
            }
            else if (!listeners.Contains(frame))
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

            listeners?.Remove(frame);
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
            if (listeners != null && listeners.RemoveAll(listener => !listener.IsAlive) != 0)
            {
                Debug.LogWarning($"{this}: Frame has a listener that was previously destroyed.");
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

        public bool SetLocalPose(in Pose newPose)
        {
            if (localPose.EqualsApprox(newPose))
            {
                return false;
            }

            localPose = newPose;
            Transform.SetLocalPose(newPose);
            return true;
        }
        
        protected override void Stop()
        {
            if (children.Count == 0)
            {
                base.Stop();
                return;
            }

            var childrenCopy = children.Values.ToArray();
            foreach (var frame in childrenCopy)
            {
                frame.Parent = null;
            }
            
            base.Stop();
        }

        public abstract void ForceInvisible();

        public abstract void Highlight();

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
    }
}