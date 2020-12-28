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
        public Material PointCloud;
        public Material PointCloudWithColormap;

        public Material LineMaterial;
        public Material TransparentLine;
        public Material LineWithColormap;
        public Material TransparentLineWithColormap;
        public Material LineSimple;
        public Material TransparentLineSimple;
        public Material LineSimpleWithColormap;
        public Material TransparentLineSimpleWithColormap;

        public Material LitOcclusionOnly;

        public Material MeshListMaterial;
        public Material MeshListWithColormap;
        public Material MeshListWithColormapScaleY;
        public Material MeshListOcclusionOnly;
        public Material MeshListOcclusionOnlyWithScaleY;
    }
}