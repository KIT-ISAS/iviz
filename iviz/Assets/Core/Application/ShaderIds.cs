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
    }
}