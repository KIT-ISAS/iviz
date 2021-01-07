using UnityEngine;
using UnityEngine.Serialization;

namespace Iviz.Displays
{
    public class AppAssetHolder : MonoBehaviour
    {
        public Texture2D AtlasLarge;
        public Texture2D AtlasLargeFlip;
        
        public GameObject ARPrefab;
        public GameObject AngleAxis;
        public GameObject ARCameraFovDisplay;
        public GameObject ARMarkerResource;
        public GameObject Arrow;
        public GameObject ArrowShort;
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
        public GameObject TFFrame;
        public GameObject Trail;
        public GameObject Triangle;

        public Material LineConnectorMaterial;
        public Material TextMaterial;
        
    }
}