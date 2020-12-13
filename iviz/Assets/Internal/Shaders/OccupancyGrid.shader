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
            //"Queue"="Transparent" "RenderType"="Transparent"
            "RenderType"="Opaque"
        }

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

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
            
            //float2 value = tex2D(_MainTex, IN.uv_MainTex).rg;
            float value = tex2D(_MainTex, IN.uv_MainTex).r;
            //clip(0.5 - value);
            
            if (value >= 0.4)
            {
                o.Albedo = 0.1;
                //o.Alpha = 0;
            }
            else
            {
                o.Albedo = tex2D(_AtlasTex, float2(value * 2, _AtlasRow));
                //o.Alpha = 1;
            }

            o.Alpha = 1;
            //o.Albedo = tex2D(_AtlasTex, float2(value.r * 2, _AtlasRow));
            //o.Alpha = value.g;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
    //FallBack "Standard"
}