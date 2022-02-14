#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Sdf;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class BoundaryCheckWidget : MonoBehaviour, IWidgetWithBoundaries, IWidgetWithScale
    {
        FrameNode[] nodes = Array.Empty<FrameNode>();
        BoxCollider[] colliders = Array.Empty<BoxCollider>();
        bool active;
        float warningDistance = 1.0f;

        readonly Dictionary<BoxCollider, BoundaryLinkDisplay> links = new();

        public bool Interactable
        {
            set { }
        }

        public float Scale
        {
            set { }
        }

        public float SecondaryScale
        {
            set => warningDistance = value;
        }

        public IReadOnlyList<BoundingBox> BoundingBoxes
        {
            set => Set(value);
        }

        void Set(IReadOnlyList<BoundingBox> value)
        {
            Reset();

            int count = value.Count;
            if (count < 2)
            {
                throw new ArgumentException("Boxes size should be greater than 1");
            }

            nodes = new FrameNode[count];
            colliders = new BoxCollider[count];

            foreach (int i in ..count)
            {
                nodes[i] = new FrameNode("BoundingBox");
                nodes[i].AttachTo(value[i].Header);
                nodes[i].Transform.SetLocalPose(value[i].Center.Ros2Unity());

                colliders[i] = nodes[i].Transform.gameObject.AddComponent<BoxCollider>();
                colliders[i].size = value[i].Size.Ros2Unity().Abs();
            }

            if (active)
            {
                return;
            }

            active = true;
            GameThread.EveryTenthOfASecond += UpdateBounds;
        }

        void Reset()
        {
            foreach (var node in nodes)
            {
                node.Dispose();
            }

            nodes = Array.Empty<FrameNode>();
            colliders = Array.Empty<BoxCollider>();

            foreach (var link in links.Values)
            {
                link.ReturnToPool();
            }

            links.Clear();

            GameThread.EveryTenthOfASecond -= UpdateBounds;
            active = false;
        }

        void UpdateBounds()
        {
            var baseCollider = colliders[0];
            foreach (var collider in colliders.Skip(1))
            {
                float distance = baseCollider.DistanceTo(collider, out _, out _);
                if (distance < warningDistance && !links.ContainsKey(collider))
                {
                    var link = ResourcePool.RentDisplay<BoundaryLinkDisplay>();
                    link.Set(baseCollider, collider);
                    links[collider] = link;
                }
                else if (distance > warningDistance * 1.1f && links.TryGetValue(collider, out var link))
                {
                    link.ReturnToPool();
                    links.Remove(collider);
                }
            }
        }

        public void Suspend()
        {
            Reset();
        }
    }
}