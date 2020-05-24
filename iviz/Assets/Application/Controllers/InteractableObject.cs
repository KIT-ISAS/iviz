using Iviz.Msgs.VisualizationMsgs;
using UnityEngine;

namespace Iviz.App.Listeners
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public InteractiveMarkerObject Parent { get; protected set; }

        public abstract InteractiveMarkerFeedback OnClick(Vector3 point, int button);

        public abstract void Select();

        public abstract void Deselect(InteractableObject newSelection);
    }
}
