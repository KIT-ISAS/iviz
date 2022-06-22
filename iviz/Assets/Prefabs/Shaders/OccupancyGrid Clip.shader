Shader "iviz/OccupancyGrid Clip"
{
    Properties
    {
        _Smoothness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metalness", Range(0,1)) = 0.5
        _AtlasTex("Atlas Texture", 2D) = "white" {}
    }
    SubShader
    {
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows addshadow

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        sampler2D _AtlasTex;
        float _AtlasRow;

        half _Smoothness;
        half _Metallic;

        float4 _Tint;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float value = tex2D(_MainTex, IN.uv_MainTex).r * (127 / 100.);
            const fixed4 color = tex2D(_AtlasTex, float2(value, _AtlasRow));
            clip(color.a - 0.5);
            o.Albedo = _Tint.rgb * color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
}