using UnityEngine;
using UnityEngine.Serialization;

namespace Iviz.Displays
{
    public sealed class AppAssetHolder : MonoBehaviour
    {
        public Texture2D AtlasLarge;
        public Texture2D AtlasLargeFlip;
        
        public GameObject ARPrefab;
        public GameObject AngleAxis;
        public GameObject ARCameraFovDisplay;
        public GameObject Arrow;
        public GameObject AxisFrame;
        public GameObject BoundaryFrame;
        public GameObject DepthImage;
        public GameObject Grid;
        public GameObject GridMap;
        public GameObject Image;
        public GameObject InteractiveControl;
        public GameObject LineConnector;
        public GameObject OccupancyGrid;
        public GameObject OccupancyGridTexture;
        public GameObject OverlayCamera;
        public GameObject Pyramid;
        public GameObject RadialScan;
        public GameObject Ring;
        public GameObject Square;
        //public GameObject TFFrame;
        public GameObject Trail;
        public GameObject Triangle;
        
        public GameObject ARDialog;
        public GameObject ARDialogIcon;
        public GameObject ARDialogMenu;
        public GameObject ARDialogShort;
        public GameObject ARTfFrame;
        public GameObject ARDialogNotice;
        public GameObject ARButtonDialog;

        public GameObject ARMarkerHighlighter;

        public GameObject RotationDisc;
        public GameObject SpringDisc;
        public GameObject SpringDisc3D;
        public GameObject TrajectoryDisc;
        public GameObject Tooltip;
        public GameObject TargetArea;
        public GameObject TooltipWidget;

        public GameObject Boundary;
        public GameObject BoundaryLink;
        public GameObject BoundaryCheck;

        public GameObject PositionDisc;
        public GameObject PositionDisc3D;
        
        public Material LineConnectorMaterial;
        public Material TextMaterial;

        public Material ImagePreview;
        public Material GridInterior;
        public Material GridInteriorSimple;
        public Material GridMapMat;
        public Material TransparentGridMap;
        public Material DepthCloud;
        public Material OccupancyGridMat;
        public Material OccupancyGridClipMat;

        public GameObject RoundedPlane;
        public GameObject Leash;
        public GameObject Reticle;
        public GameObject SelectionFrame;
        public GameObject CanvasHolder;
        public GameObject PalmCompass;

        public AudioClip Click;
        public AudioClip Screenshot;

    }
}