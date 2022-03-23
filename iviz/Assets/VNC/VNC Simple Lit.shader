Shader "iviz/Vnc Simple Lit"
{
    Properties {
         _MainTex("Color Texture", 2D) = "white" {}
        }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 150

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;

        void surf(Input IN, inout SurfaceOutput o)
        {
            float2 tex = IN.uv_MainTex; 
            fixed4 c = tex2D(_MainTex, float2(tex.x, 1-tex.y));
            o.Emission = c;
        }
        ENDCG
    }
}