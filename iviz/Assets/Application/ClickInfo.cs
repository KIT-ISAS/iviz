#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Iviz.App
{
    public sealed class ClickInfo
    {
        static readonly RaycastHit[] RaycastHitsBuffer = new RaycastHit[10];

        sealed class RaycastComparer : IComparer<RaycastHit>
        {
            public static readonly RaycastComparer Instance = new();
            public int Compare(RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance);
        }
        
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

            if (ARController.Instance == null
                || !ARController.IsVisible
                || !ARController.Instance.TryGetRaycastHits(ray, out var results)
                || results.Count == 0)
            {
                hits = Array.Empty<ClickHitResult>();
                return false;
            }

            hits = results
                .Where(result => result.trackable is ARPlane)
                .Select(result => new ClickHitResult(result))
                .ToArray();
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

            int numHits = Physics.RaycastNonAlloc(ray, RaycastHitsBuffer, 100, LayerType.RaycastLayerMask);

            Array.Sort(RaycastHitsBuffer, 0, numHits, RaycastComparer.Instance);

            cachedUnityHits = numHits == 0
                ? Array.Empty<ClickHitResult>()
                : RaycastHitsBuffer.Take(numHits)
                    .Select(result => new ClickHitResult(result))
                    .ToArray();
            hits = cachedUnityHits;

            return cachedUnityHits.Length != 0;
        }
    }
    
}