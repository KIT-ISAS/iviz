#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Iviz.Controllers
{
    public interface IWidgetFeedback
    {
        void OnWidgetRotated(string id, string frameId, float angleInRad);
        void OnWidgetMoved(string id, string frameId, in Vector3 direction);
        void OnWidgetProvidedTrajectory(string id, string frameId, List<Vector3> points);
        void OnWidgetResized(string id, string frameId, in Bounds bounds);
        void OnWidgetClicked(string id, string frameId, int entryId);
    }
}