using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public sealed class ClickInfo
    {
        readonly Vector2 cursorPosition;
        
        bool arHitSet;
        Pose? arHit;
        [CanBeNull] List<RaycastResult> cursorHits;

        public ClickInfo(in Vector2 cursorPosition) => this.cursorPosition = cursorPosition;

        public bool TryGetARRaycastHit(out Pose pose)
        {
            if (arHitSet)
            {
                if (arHit != null)
                {
                    pose = arHit.Value;
                    return true;
                }

                pose = default;
                return false;
            }

            if (ARController.Instance == null)
            {
                pose = default;
                return false;
            }

            arHitSet = true;
                
            var ray = Settings.MainCamera.ScreenPointToRay(cursorPosition);
            if (ARController.Instance.TryGetRaycastHit(ray, out var newPose))
            {
                pose = newPose;
                arHit = newPose;
                return true;
            }

            pose = default;
            arHit = null;
            return false;
        }
            
        public bool TryGetRaycastResults([NotNull] out IReadOnlyCollection<RaycastResult> hits)
        {
            if (cursorHits != null)
            {
                hits = cursorHits;
                return cursorHits.Count != 0;
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
            cursorHits = results;
            hits = cursorHits;

            raycaster.eventMask = oldMask;
            
            return cursorHits.Count != 0;
        }
            
    }
}