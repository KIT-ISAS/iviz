Shader "iviz/Simple TexturedLit"
{
    Properties
    {
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        LOD 150

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
		UNITY_INSTANCING_BUFFER_END(Props)
        
        void surf(Input IN, inout SurfaceOutput o)
        {
            const fixed4 texture_color = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb * texture_color.rgb;
        }
        ENDCG
    }
}