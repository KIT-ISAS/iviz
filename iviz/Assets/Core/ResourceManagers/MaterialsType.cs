#nullable enable

using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Resources
{
    public sealed class MaterialsType
    {
        public ResourceKey<Material> FontMaterial { get; }
        public ResourceKey<Material> FontMaterialAlwaysVisible { get; }

        public ResourceKey<Material> Lit { get; }
        public ResourceKey<Material> TransparentLit { get; }
        public ResourceKey<Material> TexturedLit { get; }
        public ResourceKey<Material> TransparentTexturedLit { get; }
        public ResourceKey<Material> BumpLit { get; }
        public ResourceKey<Material> TransparentBumpLit { get; }

        public ResourceKey<Material> SimpleLit { get; }
        public ResourceKey<Material> SimpleTransparentLit { get; }
        public ResourceKey<Material> SimpleTexturedLit { get; }
        public ResourceKey<Material> SimpleTransparentTexturedLit { get; }

        public ResourceKey<Material> LitHalfVisible { get; }
        public ResourceKey<Material> TransparentLitAlwaysVisible { get; }
        public ResourceKey<Material> ImagePreview { get; }
        public ResourceKey<Material> PointCloud { get; }
        public ResourceKey<Material> PointCloudWithColormap { get; }
        public ResourceKey<Material> PointCloudWithRosSwizzle { get; }
        public ResourceKey<Material> PointCloudWithWithRosSwizzleColormap { get; }
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
        public ResourceKey<Material> MeshListWithRosSwizzle { get; }
        public ResourceKey<Material> MeshListWithColormap { get; }
        public ResourceKey<Material> MeshListWithRosSwizzleColormap { get; }
        public ResourceKey<Material> MeshListWithColormapScaleY { get; }
        public ResourceKey<Material> MeshListWithColormapScaleAll { get; }
        public ResourceKey<Material> MeshListOcclusionOnly { get; }
        public ResourceKey<Material> MeshListOcclusionOnlyWithScaleY { get; }
        public ResourceKey<Material> MeshListOcclusionOnlyWithScaleAll { get; }

        public ResourceKey<Material> FancyTransparentLit { get; }

        public MaterialsType()
        {
            var assetHolder = ResourcePool.AssetHolder;
            var appAssetHolder = ResourcePool.AppAssetHolder;

            //Atlas = assetHolder.Atlas.AssertNotNull(nameof(assetHolder.Atlas));
            //AtlasLarge = appAssetHolder.AtlasLarge.AssertNotNull(nameof(appAssetHolder.AtlasLarge));
            //AtlasLargeFlip = appAssetHolder.AtlasLargeFlip.AssertNotNull(nameof(appAssetHolder.AtlasLargeFlip));

            FontMaterial = Create(assetHolder.FontMaterial);
            FontMaterialAlwaysVisible = Create(assetHolder.FontMaterialZWrite);

            Lit = Create(assetHolder.Lit, nameof(assetHolder.Lit));
            TexturedLit = Create(assetHolder.TexturedLit, nameof(assetHolder.TexturedLit));
            TransparentLit = Create(assetHolder.TransparentLit, nameof(assetHolder.TransparentLit));
            TransparentTexturedLit =
                Create(assetHolder.TransparentTexturedLit, nameof(assetHolder.TransparentTexturedLit));
            BumpLit = Create(assetHolder.BumpLit, nameof(assetHolder.BumpLit));
            TransparentBumpLit = Create(assetHolder.TransparentBumpLit, nameof(assetHolder.TransparentBumpLit));

            LitHalfVisible = Create(assetHolder.LitHalfVisible, nameof(assetHolder.LitHalfVisible));
            TransparentLitAlwaysVisible = Create(assetHolder.TransparentLitAlwaysVisible,
                nameof(assetHolder.TransparentLitAlwaysVisible));

            SimpleLit = Create(assetHolder.SimpleLit, nameof(assetHolder.SimpleLit));
            SimpleTransparentLit = Create(assetHolder.SimpleTransparentLit, nameof(assetHolder.SimpleTransparentLit));
            SimpleTexturedLit = Create(assetHolder.SimpleTexturedLit, nameof(assetHolder.SimpleTexturedLit));
            SimpleTransparentTexturedLit = Create(assetHolder.SimpleTransparentTexturedLit,
                nameof(assetHolder.SimpleTransparentTexturedLit));

            ImagePreview = Create(appAssetHolder.ImagePreview);
            GridInterior = Create(appAssetHolder.GridInterior);
            GridInteriorSimple = Create(appAssetHolder.GridInteriorSimple);
            GridMap = Create(appAssetHolder.GridMapMat);
            TransparentGridMap = Create(appAssetHolder.TransparentGridMap);
            DepthCloud = Create(appAssetHolder.DepthCloud);
            OccupancyGridTexture = Settings.IsMobile
                ? Create(appAssetHolder.OccupancyGridMat)
                : Create(appAssetHolder.OccupancyGridClipMat);

            PointCloud = Create(assetHolder.PointCloud, nameof(assetHolder.PointCloud));
            PointCloudWithColormap =
                Create(assetHolder.PointCloudWithColormap, nameof(assetHolder.PointCloudWithColormap));
            PointCloudWithRosSwizzle = Create(assetHolder.PointCloudWithRosSwizzle,
                nameof(assetHolder.PointCloudWithRosSwizzle));
            PointCloudWithWithRosSwizzleColormap = Create(assetHolder.PointCloudWithRosSwizzleColormap,
                nameof(assetHolder.PointCloudWithRosSwizzleColormap));

            Line = Create(assetHolder.LineMaterial, nameof(assetHolder.LineMaterial));
            TransparentLine = Create(assetHolder.TransparentLine, nameof(assetHolder.TransparentLine));
            LineSimple = Create(assetHolder.LineSimple, nameof(assetHolder.LineSimple));
            TransparentLineSimple = Create(assetHolder.TransparentLineSimple,
                nameof(assetHolder.TransparentLineSimple));

            PointCloudDirect = Create(assetHolder.PointCloudDirect);
            PointCloudDirectWithColormap = Create(assetHolder.PointCloudDirectWithColormap);
            LineWithColormap = Create(assetHolder.LineWithColormap);
            TransparentLineWithColormap = Create(assetHolder.TransparentLineWithColormap);
            LineSimpleWithColormap = Create(assetHolder.LineSimpleWithColormap);
            TransparentLineSimpleWithColormap = Create(assetHolder.TransparentLineSimpleWithColormap);

            FancyTransparentLit = Create(appAssetHolder.FancyTransparentLitMat,
                nameof(appAssetHolder.FancyTransparentLitMat));

            /*
            LinePulse = Create(Settings.SupportsComputeBuffers
                ? assetHolder.LinePulse
                : assetHolder.LinePulseSimple);
            LinePulse.Object.SetColor(ShaderIds.TintId, Color.white);
            */

            /*
            LineMesh = Create(Settings.SupportsComputeBuffers
                ? assetHolder.LineMesh
                : assetHolder.LineMeshSimple);
            LineMesh.Object.SetColor(ShaderIds.TintId, Color.white);
            */

            LinePulse = Create(assetHolder.LinePulse, nameof(assetHolder.LinePulse));
            LinePulse.Object.SetColor(ShaderIds.TintId, Color.white);

            LineMesh = Create(assetHolder.LineMesh, nameof(assetHolder.LineMesh));
            LineMesh.Object.SetColor(ShaderIds.TintId, Color.white);

            MeshList = Create(assetHolder.MeshListMaterial,
                nameof(assetHolder.MeshListMaterial));
            MeshListWithRosSwizzle = Create(assetHolder.MeshListWithRosSwizzle,
                nameof(assetHolder.MeshListWithRosSwizzle));
            MeshListWithColormap = Create(assetHolder.MeshListWithColormap,
                nameof(assetHolder.MeshListWithColormap));
            MeshListWithRosSwizzleColormap = Create(assetHolder.MeshListWithRosSwizzleColormap,
                nameof(assetHolder.MeshListWithRosSwizzleColormap));
            MeshListWithColormapScaleY = Create(assetHolder.MeshListWithColormapScaleY,
                nameof(assetHolder.MeshListWithColormapScaleY));
            MeshListWithColormapScaleY.Object.enableInstancing = true; // TODO: is this needed?
            MeshListWithColormapScaleAll = Create(assetHolder.MeshListWithColormapScaleAll,
                nameof(assetHolder.MeshListWithColormapScaleAll));
            MeshListWithColormapScaleAll.Object.enableInstancing = true;
            MeshListOcclusionOnly = Create(assetHolder.MeshListOcclusionOnly,
                nameof(assetHolder.MeshListOcclusionOnly));
            MeshListOcclusionOnlyWithScaleY = Create(assetHolder.MeshListOcclusionOnlyWithScaleY,
                nameof(assetHolder.MeshListOcclusionOnlyWithScaleY));
            MeshListOcclusionOnlyWithScaleAll = Create(assetHolder.MeshListOcclusionOnlyWithScaleAll,
                nameof(assetHolder.MeshListOcclusionOnlyWithScaleAll));

            LitOcclusionOnly = Create(assetHolder.LitOcclusionOnly, nameof(assetHolder.LitOcclusionOnly));
        }

        static ResourceKey<Material> Create(Material m, string? name = null) => new(m, name);
    }
}