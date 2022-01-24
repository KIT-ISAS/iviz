Shader "iviz/Simple TransparentTexturedLit"
{
    Properties
    {
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
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
            const fixed4 color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
            o.Albedo = color.rgb * texture_color.rgb;
            o.Alpha = color.a * texture_color.a;
        }
        ENDCG
    }
}