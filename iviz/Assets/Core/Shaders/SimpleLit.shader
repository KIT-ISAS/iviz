Shader "iviz/SimpleLit"
{
    Properties {}
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
            int dummy;
        };

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
		UNITY_INSTANCING_BUFFER_END(Props)
        
        void surf(Input _, inout SurfaceOutput o)
        {
            o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb;
        }
        ENDCG
    }

    Fallback "Mobile/VertexLit"
}