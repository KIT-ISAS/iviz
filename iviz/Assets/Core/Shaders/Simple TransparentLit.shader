Shader "iviz/Simple TransparentLit"
{
    Properties {}
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }
        LOD 150

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd alpha:fade

        struct Input
        {
            int dummy;
        };

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
		UNITY_INSTANCING_BUFFER_END(Props)
        
        void surf(Input _, inout SurfaceOutput o)
        {
		    const fixed4 color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
            o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb;
		    o.Alpha = color.a;
        }
        ENDCG
    }
}