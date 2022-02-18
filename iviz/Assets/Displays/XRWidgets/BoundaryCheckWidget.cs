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

        public float SecondaryScale
        {
            set => warningDistance = value;
        }
        
        public void Set(BoundingBoxStamped baseBox, IReadOnlyList<BoundingBoxStamped> boxes)
        {
            Reset();

            int count = boxes.Count;
            if (count == 0)
            {
                throw new ArgumentException("Boxes size should be greater than 1");
            }

            nodes = new FrameNode[count + 1];
            colliders = new BoxCollider[count + 1];
            
            Create(baseBox, out nodes[0], out colliders[0]);
            foreach (int i in ..count)
            {
                Create(boxes[i], out nodes[i + 1], out colliders[i + 1]);
            }

            
            if (active)
            {
                return;
            }

            active = true;
            GameThread.EveryTenthOfASecond += UpdateBounds;
        }

        static void Create(BoundingBoxStamped box, out FrameNode node, out BoxCollider collider)
        {
            node = new FrameNode("BoundingBox");
            node.AttachTo(box.Header);
            node.Transform.SetLocalPose(box.Boundary.Center.Ros2Unity());

            collider = node.Transform.gameObject.AddComponent<BoxCollider>();
            collider.size = box.Boundary.Size.Ros2Unity().Abs();
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