using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class MaterialsType
    {
        static readonly int Tint = Shader.PropertyToID("_Tint");

        public Info<Material> FontMaterial { get; }
        public Info<Material> FontMaterialZWrite { get; }

        public Info<Material> Lit { get; }
        public Info<Material> TexturedLit { get; }
        public Info<Material> TransparentLit { get; }
        public Info<Material> TransparentTexturedLit { get; }
        public Info<Material> TransparentLitAlwaysVisible { get; }
        public Info<Material> BumpLit { get; }
        public Info<Material> TransparentBumpLit { get; }
        public Info<Material> ImagePreview { get; }
        public Info<Material> PointCloud { get; }
        public Info<Material> PointCloudWithColormap { get; }
        public Info<Material> PointCloudDirect { get; }
        public Info<Material> PointCloudDirectWithColormap { get; }
        public Info<Material> DepthCloud { get; }
        public Info<Material> GridInterior { get; }
        public Info<Material> GridInteriorSimple { get; }
        public Info<Material> GridMap { get; }
        public Info<Material> TransparentGridMap { get; }
        public Info<Material> OccupancyGridTexture { get; }

        public Info<Material> Line { get; }
        public Info<Material> TransparentLine { get; }
        public Info<Material> LineWithColormap { get; }
        public Info<Material> TransparentLineWithColormap { get; }
        public Info<Material> LineSimple { get; }
        public Info<Material> TransparentLineSimple { get; }
        public Info<Material> LineSimpleWithColormap { get; }
        public Info<Material> TransparentLineSimpleWithColormap { get; }

        public Info<Material> LinePulse { get; }
        public Info<Material> LineMesh { get; }

        public Info<Material> LitOcclusionOnly { get; }

        public Info<Material> MeshList { get; }
        public Info<Material> MeshListWithColormap { get; }
        public Info<Material> MeshListWithColormapScaleY { get; }
        public Info<Material> MeshListWithColormapScaleAll { get; }
        public Info<Material> MeshListOcclusionOnly { get; }
        public Info<Material> MeshListOcclusionOnlyWithScaleY { get; }
        public Info<Material> MeshListOcclusionOnlyWithScaleAll { get; }

        public MaterialsType()
        {
            var assetHolder = UnityEngine.Resources.Load<GameObject>("Asset Holder").GetComponent<AssetHolder>();
            var appAssetHolder = UnityEngine.Resources.Load<GameObject>("App Asset Holder")
                .GetComponent<AppAssetHolder>();

            FontMaterial = new Info<Material>(assetHolder.FontMaterial);
            FontMaterialZWrite = new Info<Material>(assetHolder.FontMaterialZWrite);
            
            Lit = new Info<Material>(assetHolder.Lit);
            TexturedLit = new Info<Material>(assetHolder.TexturedLit);
            TransparentLit = new Info<Material>(assetHolder.TransparentLit);
            TransparentTexturedLit = new Info<Material>(assetHolder.TransparentTexturedLit);
            TransparentLitAlwaysVisible = new Info<Material>(assetHolder.TransparentLitAlwaysVisible);

            BumpLit = new Info<Material>(assetHolder.BumpLit);
            TransparentBumpLit = new Info<Material>(assetHolder.TransparentBumpLit);
            ImagePreview = new Info<Material>(appAssetHolder.ImagePreview);
            GridInterior = new Info<Material>(appAssetHolder.GridInterior);
            GridInteriorSimple = new Info<Material>(appAssetHolder.GridInteriorSimple);
            GridMap = new Info<Material>(appAssetHolder.GridMapMat);
            TransparentGridMap = new Info<Material>(appAssetHolder.TransparentGridMap);
            DepthCloud = new Info<Material>(appAssetHolder.DepthCloud);
            OccupancyGridTexture = Settings.IsMobile
                ? new Info<Material>(appAssetHolder.OccupancyGridMat)
                : new Info<Material>(appAssetHolder.OccupancyGridClipMat);

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

            LinePulse = new Info<Material>(Settings.SupportsComputeBuffers
                ? assetHolder.LinePulse
                : assetHolder.LinePulseSimple);
            LinePulse.Object.SetColor(Tint, Color.white);

            LineMesh = new Info<Material>(Settings.SupportsComputeBuffers
                ? assetHolder.LineMesh
                : assetHolder.LineMeshSimple);
            LineMesh.Object.SetColor(Tint, Color.white);

            MeshList = new Info<Material>(assetHolder.MeshListMaterial);
            MeshListWithColormap = new Info<Material>(assetHolder.MeshListWithColormap);
            MeshListWithColormapScaleY = new Info<Material>(assetHolder.MeshListWithColormapScaleY);
            MeshListWithColormapScaleY.Object.enableInstancing = true; // TODO: is this needed?
            MeshListWithColormapScaleAll = new Info<Material>(assetHolder.MeshListWithColormapScaleAll);
            MeshListWithColormapScaleAll.Object.enableInstancing = true;
            MeshListOcclusionOnly = new Info<Material>(assetHolder.MeshListOcclusionOnly);
            MeshListOcclusionOnlyWithScaleY = new Info<Material>(assetHolder.MeshListOcclusionOnlyWithScaleY);
            MeshListOcclusionOnlyWithScaleAll = new Info<Material>(assetHolder.MeshListOcclusionOnlyWithScaleAll);

            LitOcclusionOnly = new Info<Material>(assetHolder.LitOcclusionOnly);
        }
    }
}