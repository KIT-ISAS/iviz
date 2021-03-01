using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public class MaterialsType
    {
        public Info<Material> FontMaterial { get; }
        public Info<Material> FontMaterialZWrite { get; }

        public Info<Material> Lit { get; }
        public Info<Material> TexturedLit { get; }
        public Info<Material> TransparentLit { get; }
        public Info<Material> TransparentTexturedLit { get; }
        public Info<Material> BumpLit { get; }
        public Info<Material> TransparentBumpLit { get; }
        public Info<Material> ImagePreview { get; }
        public Info<Material> PointCloud { get; }
        public Info<Material> PointCloudWithColormap { get; }
        public Info<Material> PointCloudDirect { get; }
        public Info<Material> PointCloudDirectWithColormap { get; }
        public Info<Material> DepthCloud { get; }
        public Info<Material> GridInterior { get; }
        public Info<Material> GridMap { get; }
        public Info<Material> OccupancyGridTexture { get; }

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
            var assetHolder = UnityEngine.Resources.Load<GameObject>("Asset Holder").GetComponent<AssetHolder>();

            FontMaterial = new Info<Material>(assetHolder.FontMaterial);
            FontMaterialZWrite = new Info<Material>(assetHolder.FontMaterialZWrite);

            Lit = Settings.IsHololens
                ? new Info<Material>("Hololens Materials/Holo White")
                : new Info<Material>(assetHolder.Lit);
            TexturedLit = Settings.IsHololens
                ? new Info<Material>("Hololens Materials/Holo Textured Lit")
                : new Info<Material>(assetHolder.TexturedLit);
            TransparentLit = Settings.IsHololens
                ? new Info<Material>("Hololens Materials/Holo Transparent Lit")
                : new Info<Material>(assetHolder.TransparentLit);
            TransparentTexturedLit = Settings.IsHololens
                ? new Info<Material>("Hololens Materials/Holo Transparent Textured Lit")
                : new Info<Material>(assetHolder.TransparentTexturedLit);
            BumpLit = new Info<Material>(assetHolder.BumpLit);
            TransparentBumpLit = new Info<Material>(assetHolder.TransparentBumpLit);
            ImagePreview = new Info<Material>("Materials/ImagePreview");
            GridInterior = new Info<Material>("Materials/Grid Interior");
            GridMap = new Info<Material>("Materials/GridMap");
            DepthCloud = new Info<Material>("Materials/DepthCloud");
            OccupancyGridTexture = (Settings.IsMobile || Settings.IsHololens)
                ? new Info<Material>("Materials/OccupancyGrid") 
                : new Info<Material>("Materials/OccupancyGrid Clip");

            PointCloud = new Info<Material>(assetHolder.PointCloud);
            PointCloudDirect = new Info<Material>(assetHolder.PointCloudDirect);
            Line = new Info<Material>(assetHolder.LineMaterial);
            TransparentLine = new Info<Material>(assetHolder.TransparentLine);
            LineSimple = new Info<Material>(assetHolder.LineSimple);
            TransparentLineSimple = new Info<Material>(assetHolder.TransparentLineSimple);

            PointCloudWithColormap = new Info<Material>(assetHolder.PointCloudWithColormap);
            PointCloudDirectWithColormap = new Info<Material>(assetHolder.PointCloudDirectWithColormap);
            LineWithColormap = new Info<Material>(assetHolder.LineWithColormap);
            TransparentLineWithColormap = new Info<Material>(assetHolder.TransparentLineWithColormap);
            LineSimpleWithColormap = new Info<Material>(assetHolder.LineSimpleWithColormap);
            TransparentLineSimpleWithColormap = new Info<Material>(assetHolder.TransparentLineSimpleWithColormap);

            MeshList = new Info<Material>(assetHolder.MeshListMaterial);
            MeshListWithColormap = new Info<Material>(assetHolder.MeshListWithColormap);
            MeshListWithColormapScaleY = new Info<Material>(assetHolder.MeshListWithColormapScaleY);
            MeshListWithColormapScaleY.Object.enableInstancing = true;
            MeshListOcclusionOnly = new Info<Material>(assetHolder.MeshListOcclusionOnly);
            MeshListOcclusionOnlyWithScaleY = new Info<Material>(assetHolder.MeshListOcclusionOnlyWithScaleY);

            LitOcclusionOnly = new Info<Material>(assetHolder.LitOcclusionOnly);
        }
    }
}