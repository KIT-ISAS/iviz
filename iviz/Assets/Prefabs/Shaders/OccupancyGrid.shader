Shader "iviz/OccupancyGrid"
{
    Properties
    {
        _Smoothness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metalness", Range(0,1)) = 0.5
        _AtlasTex("Atlas Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows addshadow alpha:fade

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
            o.Albedo = _Tint.rgb * color.rgb;
            o.Alpha = _Tint.a * color.a;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
}