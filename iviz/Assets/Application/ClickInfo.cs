using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

namespace Iviz.App
{
    public readonly struct ClickHitResult
    {
        [NotNull] public GameObject GameObject { get; }
        public Vector3 Position { get; }
        public Vector3 Normal { get; }

        ClickHitResult([NotNull] GameObject gameObject, in Vector3 position, in Vector3 normal)
        {
            GameObject = gameObject;
            Position = position;
            Normal = normal;
        }

        public ClickHitResult(in RaycastResult r) : this(r.gameObject, r.worldPosition, r.worldNormal)
        {
        }

        public ClickHitResult(in ARRaycastHit r) : this(r.trackable.gameObject, r.pose.position,
            ((ARPlane)r.trackable).normal)
        {
        }

        public Pose CreatePose()
        {
            var side = Mathf.Approximately(Vector3.forward.Cross(Normal).MagnitudeSq(), 0)
                ? Vector3.right
                : Vector3.forward;
            
            var forward = side.Cross(Normal);
            
            return new Pose(Position, Quaternion.LookRotation(forward, Normal));
        } 
        
    }

    public sealed class ClickInfo
    {
        readonly Vector2 cursorPosition;

        [CanBeNull] ClickHitResult[] arHits;
        [CanBeNull] ClickHitResult[] unityHits;

        public ClickInfo(in Vector2 cursorPosition) => this.cursorPosition = cursorPosition;

        public bool TryGetARRaycastResults([NotNull] out ClickHitResult[] hits)
        {
            if (arHits != null)
            {
                hits = arHits;
                return hits.Length != 0;
            }

            if (ARController.Instance == null || !ARController.InstanceVisible)
            {
                hits = Array.Empty<ClickHitResult>();
                return false;
            }

            var ray = Settings.MainCamera.ScreenPointToRay(cursorPosition);
            if (!ARController.Instance.TryGetRaycastHits(ray, out var results) || results.Count == 0)
            {
                hits = Array.Empty<ClickHitResult>();
                return false;
            }

            hits = results
                .Where(result => result.trackable is ARPlane)
                .Select(result => new ClickHitResult(result)).ToArray();
            arHits = hits;
            return true;
        }

        public bool TryGetRaycastResults([NotNull] out ClickHitResult[] hits)
        {
            if (unityHits != null)
            {
                hits = unityHits;
                return hits.Length != 0;
            }

            var raycaster = Settings.MainCamera.GetComponent<PhysicsRaycaster>();
            var oldMask = raycaster.eventMask;

            raycaster.eventMask = oldMask | (1 << LayerType.Default) | (1 << LayerType.Collider);

            var results = new List<RaycastResult>();
            var eventData = new PointerEventData(null)
            {
                position = cursorPosition
            };

            raycaster.Raycast(eventData, results);
            raycaster.eventMask = oldMask;

            unityHits = results.Count == 0
                ? Array.Empty<ClickHitResult>()
                : results.Select(result => new ClickHitResult(result)).ToArray();
            hits = unityHits;

            return unityHits.Length != 0;
        }
    }
}