#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Sdf;
using Iviz.Tools;
using UnityEngine;

/*
namespace Iviz.Displays.XR
{
    public sealed class BoundaryCheckWidget : MonoBehaviour, IWidgetWithBoundaries, IWidgetWithSecondScale
    {
        FrameNode? mainNode;
        BoxCollider? mainCollider;
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
                throw new ArgumentException("Boxes are empty");
            }

            Create(baseBox, out mainNode, out mainCollider);

            nodes = new FrameNode[count];
            colliders = new BoxCollider[count];
            
            foreach (int i in ..count)
            {
                Create(boxes[i], out nodes[i], out colliders[i]);
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
            mainNode?.Dispose();
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
            if (mainCollider == null)
            {
                return;
            }
            
            var baseCollider = mainCollider;
            foreach (var otherCollider in colliders)
            {
                float distance = BoundaryLinkDisplay.DistanceTo(baseCollider, otherCollider, out _, out _);
                if (distance < warningDistance && !links.ContainsKey(otherCollider))
                {
                    var link = ResourcePool.RentDisplay<BoundaryLinkDisplay>();
                    link.Set(baseCollider, otherCollider);
                    links[otherCollider] = link;
                }
                else if (distance > warningDistance * 1.1f && links.TryGetValue(otherCollider, out var link))
                {
                    link.ReturnToPool();
                    links.Remove(otherCollider);
                }
            }
        }

        public void Suspend()
        {
            Reset();
        }
    }
}
*/