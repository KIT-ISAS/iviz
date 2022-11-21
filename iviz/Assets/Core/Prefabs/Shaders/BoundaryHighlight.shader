Shader "iviz/BoundaryHighlight"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,0.0)
        _Size ("Size", Range(0,1.0)) = 0.01
        _MinAlpha ("Min Alpha", Range(0,1.0)) = 0
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        fixed4 _Color;
        float _Size;
        float _MinAlpha;
        
        float3 center;

        struct Input
        {
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float3 diff = IN.worldPos - center;
            fixed dist = dot(diff, diff);
            fixed t = 1 - clamp(dist / _Size, 0, 1);
            fixed t2 = t * t;
            fixed t4 = t2 * t2;
            o.Albedo = _Color.rgb;
            o.Alpha = lerp(_MinAlpha, 1, t4) * _Color.a;
            o.Emission = _Color.rgb;
        }
        ENDCG
    }
}