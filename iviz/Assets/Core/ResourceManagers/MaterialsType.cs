using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class MaterialsType
    {
        static readonly int Tint = Shader.PropertyToID("_Tint");

        public Texture2D Atlas { get; }
        public Texture2D AtlasLarge { get; }
        public Texture2D AtlasLargeFlip { get; }
        
        public ResourceKey<Material> FontMaterial { get; }
        public ResourceKey<Material> FontMaterialZWrite { get; }

        public ResourceKey<Material> Lit { get; }
        public ResourceKey<Material> TexturedLit { get; }
        public ResourceKey<Material> TransparentLit { get; }
        public ResourceKey<Material> TransparentTexturedLit { get; }
        public ResourceKey<Material> TransparentLitAlwaysVisible { get; }
        public ResourceKey<Material> BumpLit { get; }
        public ResourceKey<Material> TransparentBumpLit { get; }
        public ResourceKey<Material> ImagePreview { get; }
        public ResourceKey<Material> PointCloud { get; }
        public ResourceKey<Material> PointCloudWithColormap { get; }
        public ResourceKey<Material> PointCloudDirect { get; }
        public ResourceKey<Material> PointCloudDirectWithColormap { get; }
        public ResourceKey<Material> DepthCloud { get; }
        public ResourceKey<Material> GridInterior { get; }
        public ResourceKey<Material> GridInteriorSimple { get; }
        public ResourceKey<Material> GridMap { get; }
        public ResourceKey<Material> TransparentGridMap { get; }
        public ResourceKey<Material> OccupancyGridTexture { get; }

        public ResourceKey<Material> Line { get; }
        public ResourceKey<Material> TransparentLine { get; }
        public ResourceKey<Material> LineWithColormap { get; }
        public ResourceKey<Material> TransparentLineWithColormap { get; }
        public ResourceKey<Material> LineSimple { get; }
        public ResourceKey<Material> TransparentLineSimple { get; }
        public ResourceKey<Material> LineSimpleWithColormap { get; }
        public ResourceKey<Material> TransparentLineSimpleWithColormap { get; }

        public ResourceKey<Material> LinePulse { get; }
        public ResourceKey<Material> LineMesh { get; }

        public ResourceKey<Material> LitOcclusionOnly { get; }

        public ResourceKey<Material> MeshList { get; }
        public ResourceKey<Material> MeshListWithColormap { get; }
        public ResourceKey<Material> MeshListWithColormapScaleY { get; }
        public ResourceKey<Material> MeshListWithColormapScaleAll { get; }
        public ResourceKey<Material> MeshListOcclusionOnly { get; }
        public ResourceKey<Material> MeshListOcclusionOnlyWithScaleY { get; }
        public ResourceKey<Material> MeshListOcclusionOnlyWithScaleAll { get; }

        public MaterialsType()
        {
            var assetHolder = Resource.Extras.AssetHolder;
            var appAssetHolder = Resource.Extras.AppAssetHolder;

            Atlas = assetHolder.Atlas.AssertNotNull(nameof(assetHolder.Atlas));
            AtlasLarge = appAssetHolder.AtlasLarge.AssertNotNull(nameof(appAssetHolder.AtlasLarge));
            AtlasLargeFlip = appAssetHolder.AtlasLargeFlip.AssertNotNull(nameof(appAssetHolder.AtlasLargeFlip));
            
            FontMaterial = Create(assetHolder.FontMaterial);
            FontMaterialZWrite = Create(assetHolder.FontMaterialZWrite);
            
            Lit = Create(assetHolder.Lit);
            TexturedLit = Create(assetHolder.TexturedLit);
            TransparentLit = Create(assetHolder.TransparentLit);
            TransparentTexturedLit = Create(assetHolder.TransparentTexturedLit);
            TransparentLitAlwaysVisible = Create(assetHolder.TransparentLitAlwaysVisible);

            BumpLit = Create(assetHolder.BumpLit);
            TransparentBumpLit = Create(assetHolder.TransparentBumpLit);
            ImagePreview = Create(appAssetHolder.ImagePreview);
            GridInterior = Create(appAssetHolder.GridInterior);
            GridInteriorSimple = Create(appAssetHolder.GridInteriorSimple);
            GridMap = Create(appAssetHolder.GridMapMat);
            TransparentGridMap = Create(appAssetHolder.TransparentGridMap);
            DepthCloud = Create(appAssetHolder.DepthCloud);
            OccupancyGridTexture = Settings.IsMobile
                ? Create(appAssetHolder.OccupancyGridMat)
                : Create(appAssetHolder.OccupancyGridClipMat);

            PointCloud = Create(assetHolder.PointCloud);
            PointCloudDirect = Create(assetHolder.PointCloudDirect);
            Line = Create(assetHolder.LineMaterial);
            TransparentLine = Create(assetHolder.TransparentLine);
            LineSimple = Create(assetHolder.LineSimple);
            TransparentLineSimple = Create(assetHolder.TransparentLineSimple);

            PointCloudWithColormap = Create(assetHolder.PointCloudWithColormap);
            PointCloudDirectWithColormap = Create(assetHolder.PointCloudDirectWithColormap);
            LineWithColormap = Create(assetHolder.LineWithColormap);
            TransparentLineWithColormap = Create(assetHolder.TransparentLineWithColormap);
            LineSimpleWithColormap = Create(assetHolder.LineSimpleWithColormap);
            TransparentLineSimpleWithColormap = Create(assetHolder.TransparentLineSimpleWithColormap);

            LinePulse = Create(Settings.SupportsComputeBuffers
                ? assetHolder.LinePulse
                : assetHolder.LinePulseSimple);
            LinePulse.Object.SetColor(Tint, Color.white);

            LineMesh = Create(Settings.SupportsComputeBuffers
                ? assetHolder.LineMesh
                : assetHolder.LineMeshSimple);
            LineMesh.Object.SetColor(Tint, Color.white);

            MeshList = Create(assetHolder.MeshListMaterial);
            MeshListWithColormap = Create(assetHolder.MeshListWithColormap);
            MeshListWithColormapScaleY = Create(assetHolder.MeshListWithColormapScaleY);
            MeshListWithColormapScaleY.Object.enableInstancing = true; // TODO: is this needed?
            MeshListWithColormapScaleAll = Create(assetHolder.MeshListWithColormapScaleAll);
            MeshListWithColormapScaleAll.Object.enableInstancing = true;
            MeshListOcclusionOnly = Create(assetHolder.MeshListOcclusionOnly);
            MeshListOcclusionOnlyWithScaleY = Create(assetHolder.MeshListOcclusionOnlyWithScaleY);
            MeshListOcclusionOnlyWithScaleAll = Create(assetHolder.MeshListOcclusionOnlyWithScaleAll);

            LitOcclusionOnly = Create(assetHolder.LitOcclusionOnly);

            static ResourceKey<Material> Create(Material m) => new(m);
        }
    }
}