#nullable enable

using System;
using System.Linq;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class RobotLinkHighlightable : MonoBehaviour, IHighlightable, IHasBounds
    {
        Collider Collider
        {
            set
            {
                Bounds = value switch
                {
                    BoxCollider box => box.GetLocalBounds(),
                    SphereCollider sphere => new Bounds(sphere.center, 2 * sphere.radius * Vector3.one),
                    MeshCollider mesh => mesh.sharedMesh is { } subMesh ? subMesh.bounds : null,
                    _ => null
                };
                BoundsChanged?.Invoke();
            }
        }

        void OnDestroy()
        {
            BoundsChanged = null;
        }

        public bool IsAlive => true;
        public Bounds? Bounds { get; private set; }
        public Transform BoundsTransform => transform;
        public event Action? BoundsChanged;
        public string Caption { get; private set; } = "";

        public void Highlight(in Vector3 _)
        {
            FAnimator.Start(new BoundsHighlighter(this, duration: 1));
        }

        public static void ProcessRobot(string robotName, GameObject baseLink)
        {
            string robotNameCaption = $"<b>{robotName}</b>\n";
            /*
            var nodes = baseLink.GetComponentsInChildren<Transform>()
                .Where(transform => transform.CompareTag(RobotModel.ColliderTag));
                */
            var nodes = baseLink.transform.GetAllChildren()
                .Where(transform => transform.CompareTag(RobotModel.ColliderTag));
            foreach (var node in nodes)
            {
                string linkName = node.parent.name;
                if (node.TryGetComponent<Collider>(out var collider))
                {
                    var highlightable = node.gameObject.AddComponent<RobotLinkHighlightable>();
                    highlightable.Collider = collider;
                    highlightable.Caption = robotNameCaption + linkName;
                }
                else
                {
                    //var colliders = node.GetComponentsInChildren<Collider>();
                    var colliders = node.GetAllChildren().WithComponent<Collider>();
                    foreach (var subCollider in colliders)
                    {
                        var highlightable = subCollider.gameObject.AddComponent<RobotLinkHighlightable>();
                        highlightable.Collider = subCollider;
                        highlightable.Caption = robotNameCaption + linkName;
                    }
                }
            }
        }
    }
}