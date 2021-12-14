using UnityEngine;

namespace Iviz.Displays
{
    public class AssetHolder : MonoBehaviour
    {
        public GameObject Cube = null;
        public GameObject Cylinder = null;
        public GameObject Line = null;
        public GameObject MeshList = null;
        public GameObject MeshTriangles = null;
        public GameObject Plane = null;
        public GameObject PointList = null;
        public GameObject Sphere = null;
        public GameObject Text = null;

        public Material FontMaterial;
        public Material FontMaterialZWrite;

        public Material Lit;
        public Material TexturedLit;
        public Material TransparentLit;
        public Material TransparentTexturedLit;
        public Material TransparentLitAlwaysVisible;
        public Material BumpLit;
        public Material TransparentBumpLit;
        public Material PointCloud;
        public Material PointCloudWithColormap;
        public Material PointCloudDirect;
        public Material PointCloudDirectWithColormap;

        public Material LineMaterial;
        public Material TransparentLine;
        public Material LineWithColormap;
        public Material TransparentLineWithColormap;
        public Material LineSimple;
        public Material TransparentLineSimple;
        public Material LineSimpleWithColormap;
        public Material TransparentLineSimpleWithColormap;

        public Material LineMesh;
        public Material LinePulse;
        public Material LineMeshSimple;
        public Material LinePulseSimple;

        public Material LitOcclusionOnly;

        public Material MeshListMaterial;
        public Material MeshListWithColormap;
        public Material MeshListWithColormapScaleY;
        public Material MeshListWithColormapScaleAll;
        public Material MeshListOcclusionOnly;
        public Material MeshListOcclusionOnlyWithScaleY;
        public Material MeshListOcclusionOnlyWithScaleAll;
        
        public Texture2D Atlas;
        public Texture2D Lines;
        public Texture2D Pink;
        public Texture2D Copper;
        public Texture2D Bone;
        public Texture2D Gray;
        public Texture2D Winter;
        public Texture2D Autumn;
        public Texture2D Summer;
        public Texture2D Spring;
        public Texture2D Cool;
        public Texture2D Hot;
        public Texture2D Hsv;
        public Texture2D Jet;
        public Texture2D Parula;
        
        public Font BaseFont;
    }
}