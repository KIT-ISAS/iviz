Shader "iviz/LineSimple"
{
    Properties
    {
        _MainTex ("Atlas Texture", 2D) = "defaulttexture" {}
        [Toggle(USE_TEXTURE)] _UseTexture("Use Texture", Float) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        CGPROGRAM
        #pragma surface surf NoLighting noforwardadd noambient
        #pragma multi_compile _ USE_TEXTURE

        float _IntensityCoeff;
        float _IntensityAdd;

        struct Input
        {
            #if USE_TEXTURE
            float2 uv_MainTex : TEXCOORD0;
            #else
            fixed4 color : COLOR;
            #endif
        };

        float _AtlasRow;
        sampler2D _MainTex;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Tint)
        UNITY_INSTANCING_BUFFER_END(Props)

        half4 LightingNoLighting(SurfaceOutput s, half3 _, half __)
        {
            half4 c;
            c.rgb = s.Albedo;
            c.a = 1;
            return c;
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            #if USE_TEXTURE
			float intensity = IN.uv_MainTex.x * _IntensityCoeff + _IntensityAdd;
            fixed4 color = tex2D(_MainTex, float2(intensity, _AtlasRow)) * UNITY_ACCESS_INSTANCED_PROP(Props, _Tint);
            #else
            fixed4 color = IN.color * UNITY_ACCESS_INSTANCED_PROP(Props, _Tint);
            #endif
            o.Albedo = color.rgb;
            o.Alpha = 1;
        }
        ENDCG
    }
}