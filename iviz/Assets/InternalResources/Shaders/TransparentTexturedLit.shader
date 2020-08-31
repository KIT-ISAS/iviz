Shader "iviz/TransparentTexturedLit"
{
    Properties
    {
        _MainTex("Color Texture", 2D) = "white" {}
        _Color("Diffuse Color", Color) = (1,1,1,1)
        _Smoothness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metalness", Range(0,1)) = 0.5
    }
        SubShader
    {
		Tags { "Queue"="Transparent" "RenderType"="Transparent"}
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows addshadow alpha
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        float _Smoothness;
        float _Metallic;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
        UNITY_DEFINE_INSTANCED_PROP(float4, _MainTex_ST_)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _EmissiveColor)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o) {
            fixed4 albedoColor = UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
            float4 st = UNITY_ACCESS_INSTANCED_PROP(Props, _MainTex_ST_);
            fixed4 textureColor = tex2D(_MainTex, IN.uv_MainTex * st.xy + st.zw);
            o.Albedo = albedoColor.rgb * textureColor.rgb;
            o.Smoothness = _Smoothness;
            o.Alpha = albedoColor.a * textureColor.a;
            o.Metallic = _Metallic * o.Alpha;
            o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
        }
        ENDCG
    }

        FallBack "Standard"
}

