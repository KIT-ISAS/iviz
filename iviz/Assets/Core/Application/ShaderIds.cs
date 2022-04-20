using UnityEngine;

namespace Iviz.Displays
{
    public static class ShaderIds
    {
        public static readonly int PointsId = Shader.PropertyToID("_Points");
        public static readonly int ScaleId = Shader.PropertyToID("_Scale");
        public static readonly int LinesId = Shader.PropertyToID("_Lines");

        public static readonly int BoundaryCenterId = Shader.PropertyToID("_BoundaryCenter");
        public static readonly int LocalScaleId = Shader.PropertyToID("_LocalScale");
        public static readonly int LocalOffsetId = Shader.PropertyToID("_LocalOffset");
        
        public static readonly int MainTexId = Shader.PropertyToID("_MainTex");
        
        public static readonly int ColorTextureId = Shader.PropertyToID("_ColorTexture");
        public static readonly int DepthTextureId = Shader.PropertyToID("_DepthTexture");

        public static readonly int PointSizeId = Shader.PropertyToID("_PointSize");
        public static readonly int PosStId = Shader.PropertyToID("_Pos_ST");
        public static readonly int LocalToWorldId = Shader.PropertyToID("_LocalToWorld");
        public static readonly int WorldToLocalId = Shader.PropertyToID("_WorldToLocal");
        public static readonly int DepthScaleId = Shader.PropertyToID("_DepthScale");

        public static readonly int IntensityCoeffId = Shader.PropertyToID("_IntensityCoeff");
        public static readonly int IntensityAddId = Shader.PropertyToID("_IntensityAdd");
        public static readonly int AtlasRowId = Shader.PropertyToID("_AtlasRow");        
        
        public static readonly int ColorId = Shader.PropertyToID("_Color");
        public static readonly int EmissiveColorId = Shader.PropertyToID("_EmissiveColor");
        public static readonly int MainTexStId = Shader.PropertyToID("_MainTex_ST_");
        public static readonly int BumpMapStId = Shader.PropertyToID("_BumpMap_ST_");
        public static readonly int SmoothnessId = Shader.PropertyToID("_Smoothness");
        public static readonly int MetallicId = Shader.PropertyToID("_Metallic");
        
        public static readonly int BumpMapId = Shader.PropertyToID("_BumpMap");
    }
}