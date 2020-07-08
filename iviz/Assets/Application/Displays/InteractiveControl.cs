using System;
using Iviz.App;
using Iviz.Displays;
using UnityEngine;

namespace Application.Displays
{
    public class InteractiveControl : MonoBehaviour
    {
        public enum Orientation
        {
            Inherit,
            Fixed,
            ViewFacing
        }

        public enum InteractionModeType
        {
            MoveAxisX,
            MovePlaneYZ,
            RotateAxisX,
            MovePlaneYZ_RotateAxisX,
            Frame,

            Move3D,
            Rotate3D,
            MoveRotate3D
        }

        [SerializeField] MeshMarkerResource arrowPX = null;
        [SerializeField] MeshMarkerResource arrowMX = null;
        [SerializeField] MeshMarkerResource arrowPY = null;
        [SerializeField] MeshMarkerResource arrowMY = null;
        [SerializeField] MeshMarkerResource arrowPZ = null;
        [SerializeField] MeshMarkerResource arrowMZ = null;

        [SerializeField] MeshMarkerResource ringX = null;
        [SerializeField] MeshMarkerResource ringY = null;
        [SerializeField] MeshMarkerResource ringZ = null;

        [SerializeField] MeshMarkerResource ringXPlane = null;
        [SerializeField] MeshMarkerResource ringZPlane = null;

        MeshMarkerResource[] allResources;

        public event Action<Pose> Moved;

        InteractionModeType interactionMode;
        public InteractionModeType InteractionMode
        {
            get => interactionMode;
            set
            {
                interactionMode = value;

                foreach (MeshMarkerResource resource in allResources)
                {
                    resource.Visible = false;
                    resource.GetComponent<IDraggable>().Moved += OnMoved;
                }

                switch (InteractionMode)
                {
                    case InteractionModeType.MoveAxisX:
                        arrowPX.Visible = true;
                        arrowMX.Visible = true;
                        break;
                    case InteractionModeType.MovePlaneYZ:
                        arrowPY.Visible = true;
                        arrowMY.Visible = true;
                        arrowPZ.Visible = true;
                        arrowMZ.Visible = true;
                        ringX.Visible = true;
                        break;
                    case InteractionModeType.RotateAxisX:
                        ringX.Visible = true;
                        break;
                    case InteractionModeType.MovePlaneYZ_RotateAxisX:
                        arrowPY.Visible = true;
                        arrowMY.Visible = true;
                        arrowPZ.Visible = true;
                        arrowMZ.Visible = true;
                        ringX.Visible = true;
                        ringXPlane.Visible = true;
                        break;
                    case InteractionModeType.Frame:
                        arrowPX.Visible = true;
                        arrowMX.Visible = true;
                        arrowPY.Visible = true;
                        arrowMY.Visible = true;
                        arrowPZ.Visible = true;
                        arrowMZ.Visible = true;
                        ringZ.Visible = true;
                        ringZPlane.Visible = true;
                        break;
                    case InteractionModeType.Move3D:
                        arrowPX.Visible = true;
                        arrowMX.Visible = true;
                        arrowPY.Visible = true;
                        arrowMY.Visible = true;
                        arrowPZ.Visible = true;
                        arrowMZ.Visible = true;
                        ringZPlane.Visible = true;
                        break;                    
                }
            }
        }

        void OnMoved(Pose pose)
        {
            Moved?.Invoke(pose);
        }

        void Awake()
        {
            allResources = new[]
                {arrowPX, arrowMX, arrowPY, arrowMY, arrowPZ, arrowMZ, ringX, ringY, ringZ, ringXPlane, ringZPlane};

            InteractionMode = InteractionModeType.Frame;
        }
    }
}