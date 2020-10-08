using UnityEngine;

namespace Iviz.Resources
{
    public class MaterialsType
    {
        public Info<Material> Lit { get; }
        public Info<Material> SimpleLit { get; }
        public Info<Material> TexturedLit { get; }
        public Info<Material> TransparentLit { get; }
        public Info<Material> TransparentTexturedLit { get; }
        public Info<Material> ImagePreview { get; }
        public Info<Material> PointCloud { get; }
        public Info<Material> PointCloudWithColormap { get; }
        public Info<Material> DepthImageProjector { get; }
        public Info<Material> Grid { get; }
        public Info<Material> GridInterior { get; }
        public Info<Material> GridMap { get; }

        public Info<Material> Line { get; }
        public Info<Material> TransparentLine { get; }
        public Info<Material> LineWithColormap { get; }
        public Info<Material> TransparentLineWithColormap { get; }
        public Info<Material> LineSimple { get; }
        public Info<Material> TransparentLineSimple { get; }
        public Info<Material> LineSimpleWithColormap { get; }
        public Info<Material> TransparentLineSimpleWithColormap { get; }

        public Info<Material> LitOcclusionOnly { get; }

        public Info<Material> MeshList { get; }
        public Info<Material> MeshListWithColormap { get; }
        public Info<Material> MeshListWithColormapScaleY { get; }
        public Info<Material> MeshListOcclusionOnly { get; }
        public Info<Material> MeshListOcclusionOnlyWithScaleY { get; }

        public MaterialsType()
        {
            SimpleLit = new Info<Material>("Materials/SimpleWhite");
            Lit = new Info<Material>("Materials/White");
            TexturedLit = new Info<Material>("Materials/Textured Lit");
            TransparentLit = new Info<Material>("Materials/Transparent Lit");
            TransparentTexturedLit = new Info<Material>("Materials/Transparent Textured Lit");
            ImagePreview = new Info<Material>("Materials/ImagePreview");
            Grid = new Info<Material>("Materials/Grid");
            GridInterior = new Info<Material>("Materials/Grid Interior");
            GridMap = new Info<Material>("Materials/GridMap");
            DepthImageProjector = new Info<Material>("Materials/DepthImage Material");

            PointCloud = new Info<Material>("Materials/PointCloud Material");
            Line = new Info<Material>("Materials/Lines/Line");
            TransparentLine = new Info<Material>("Materials/Lines/Transparent Line");
            LineSimple = new Info<Material>("Materials/Lines/LineSimple");
            TransparentLineSimple = new Info<Material>("Materials/Lines/Transparent LineSimple");

            PointCloudWithColormap = new Info<Material>("Materials/PointCloud Material with Colormap");
            //PointCloudWithColormap.Object.EnableKeyword("USE_TEXTURE");
            LineWithColormap = new Info<Material>("Materials/Lines/Line with Colormap");
            //LineWithColormap.Object.EnableKeyword("USE_TEXTURE");
            TransparentLineWithColormap = new Info<Material>("Materials/Lines/Transparent Line with Colormap");
            //TransparentLineWithColormap.Object.EnableKeyword("USE_TEXTURE");
            LineSimpleWithColormap = new Info<Material>("Materials/Lines/LineSimple with Colormap");
            //LineSimpleWithColormap.Object.EnableKeyword("USE_TEXTURE");
            TransparentLineSimpleWithColormap = new Info<Material>("Materials/Lines/Transparent LineSimple with Colormap");
            //TransparentLineSimpleWithColormap.Object.EnableKeyword("USE_TEXTURE");

            MeshList = new Info<Material>("Materials/MeshList");
            MeshListWithColormap = new Info<Material>("Materials/MeshList with Colormap");
            //MeshListWithColormap.Object.EnableKeyword("USE_TEXTURE");
            MeshListWithColormapScaleY = new Info<Material>("Materials/MeshList with Colormap ScaleY");
            MeshListWithColormapScaleY.Object.enableInstancing = true;
            //MeshListWithColormapScaleY.Object.EnableKeyword("USE_TEXTURE_SCALE");
            MeshListOcclusionOnly = new Info<Material>("Materials/MeshList OcclusionOnly");
            MeshListOcclusionOnlyWithScaleY = new Info<Material>("Materials/MeshList OcclusionOnly with ScaleY");

            LitOcclusionOnly = new Info<Material>("Materials/White OcclusionOnly");
        }
    }
}