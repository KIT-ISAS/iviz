#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class RobotLinkHighlightable : MonoBehaviour, IHighlightable, IHasBounds
    {
        IReadOnlyList<RobotLinkHighlightable>? peers;

        Collider Collider
        {
            set
            {
                Bounds = value switch
                {
                    BoxCollider box => new Bounds(box.center, box.size),
                    SphereCollider sphere => new Bounds(sphere.center, 2 * sphere.radius * Vector3.one),
                    MeshCollider mesh => mesh.sharedMesh is { } subMesh ? subMesh.bounds : null,
                    _ => null
                };
                BoundsChanged?.Invoke();
            }
        }

        public void Highlight()
        {
            FAnimator.Start(new BoundsHighlighter(this));
            /*
            if (peers == null)
            {
                FAnimator.Start(new BoundsHighlighter(this));
            }
            else
            {
                foreach (var peer in peers)
                {
                    FAnimator.Start(new BoundsHighlighter(peer));
                }
            }
            */
        }

        public bool IsAlive => true;
        public Bounds? Bounds { get; private set; }
        public Transform BoundsTransform => transform;
        public event Action? BoundsChanged;
        public string? Caption { get; private set; }

        public static void ProcessRobot(GameObject baseLink)
        {
            var nodes = baseLink.GetComponentsInChildren<Transform>()
                .Where(transform => transform.CompareTag(RobotModel.ColliderTag));
            foreach (var node in nodes)
            {
                string linkName = node.parent.name;
                if (node.TryGetComponent<Collider>(out var collider))
                {
                    var highlightable = node.gameObject.AddComponent<RobotLinkHighlightable>();
                    highlightable.Collider = collider;
                    highlightable.Caption = linkName;
                }
                else
                {
                    var colliders = node.GetComponentsInChildren<Collider>();
                    var peers = new List<RobotLinkHighlightable>();

                    foreach (var subCollider in colliders)
                    {
                        var highlightable = subCollider.gameObject.AddComponent<RobotLinkHighlightable>();
                        highlightable.Collider = subCollider;
                        highlightable.Caption = linkName;
                        peers.Add(highlightable);
                    }

                    foreach (var peer in peers)
                    {
                        peer.peers = peers;
                    }
                }
            }
        }
    }
}