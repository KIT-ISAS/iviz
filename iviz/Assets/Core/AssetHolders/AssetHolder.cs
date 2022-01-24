using UnityEngine;

namespace Iviz.Displays
{
    public class AssetHolder : MonoBehaviour
    {
        public GameObject Cube;
        public GameObject Cylinder;
        public GameObject Line;
        public GameObject MeshList;
        public GameObject MeshTriangles;
        public GameObject Plane;
        public GameObject PointList;
        public GameObject Sphere;
        public GameObject Text;

        public Material FontMaterial;
        public Material FontMaterialZWrite;

        public Material Lit;
        public Material TransparentLit;
        public Material TexturedLit;
        public Material TransparentTexturedLit;
        public Material TransparentLitAlwaysVisible;
        public Material BumpLit;
        public Material TransparentBumpLit;

        public Material SimpleLit;
        public Material SimpleTransparentLit;
        public Material SimpleTexturedLit;
        public Material SimpleTransparentTexturedLit;

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