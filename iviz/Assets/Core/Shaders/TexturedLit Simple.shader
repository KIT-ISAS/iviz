Shader "iviz/TexturedLit Simple"
{
    Properties
    {
        _MainTex("Color Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows

        sampler2D _MainTex;

        struct Input
        {
            float4 color : COLOR;
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            const fixed4 texture_color = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = 0.5 * texture_color.rgb;
            o.Emission = 0;
        }
        ENDCG
    }

    FallBack "Standard"
}