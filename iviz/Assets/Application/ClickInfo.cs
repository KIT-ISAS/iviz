#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

namespace Iviz.App
{
    public sealed class ClickHitResult
    {
        public GameObject GameObject { get; }
        public Vector3 Position { get; }
        public Vector3 Normal { get; }

        ClickHitResult(GameObject gameObject, in Vector3 position, in Vector3 normal)
        {
            GameObject = gameObject.CheckedNull() ?? throw new ArgumentNullException(nameof(gameObject));
            Position = position;
            Normal = normal;
        }

        public ClickHitResult(in RaycastHit r) : this(r.collider.gameObject, r.point, r.normal)
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

        public void Deconstruct(out GameObject gameObject, out Vector3 position, out Vector3 normal) =>
            (gameObject, position, normal) = (GameObject, Position, Normal);
    }

    public sealed class ClickInfo
    {
        static readonly RaycastHit[] RaycastHitsBuffer = new RaycastHit[10]; 

        readonly Ray ray;
        ClickHitResult[]? cachedARHits;
        ClickHitResult[]? cachedUnityHits;

        public ClickInfo(in Vector2 cursorPosition) => ray = Settings.MainCamera.ScreenPointToRay(cursorPosition);

        public ClickInfo(in Ray ray) => this.ray = ray;

        public bool TryGetARRaycastResults(out ClickHitResult[] hits)
        {
            if (cachedARHits != null)
            {
                hits = cachedARHits;
                return hits.Length != 0;
            }

            if (ARController.Instance == null || !ARController.IsVisible)
            {
                hits = Array.Empty<ClickHitResult>();
                return false;
            }

            if (!ARController.Instance.TryGetRaycastHits(ray, out var results) || results.Count == 0)
            {
                hits = Array.Empty<ClickHitResult>();
                return false;
            }

            hits = results
                .Where(result => result.trackable is ARPlane)
                .Select(result => new ClickHitResult(result)).ToArray();
            cachedARHits = hits;
            return true;
        }

        public bool TryGetRaycastResults(out ClickHitResult[] hits)
        {
            if (cachedUnityHits != null)
            {
                hits = cachedUnityHits;
                return hits.Length != 0;
            }

            const int layerMask = (1 << LayerType.Collider) 
                                  | (1 << LayerType.Clickable) 
                                  | (1 << LayerType.TfAxis);
            int numHits = Physics.RaycastNonAlloc(ray, RaycastHitsBuffer, 100, layerMask);

            cachedUnityHits = numHits == 0
                ? Array.Empty<ClickHitResult>()
                : RaycastHitsBuffer.Take(numHits).Select(result => new ClickHitResult(result)).ToArray();
            hits = cachedUnityHits;
            
            return cachedUnityHits.Length != 0;
        }
    }
}