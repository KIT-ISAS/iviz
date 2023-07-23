Shader "iviz/FancyTransparentLit"
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
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        struct Input
        {
            fixed4 color : COLOR;
            float3 worldNormal;
            float3 viewDir;            
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _EmissiveColor)
        UNITY_DEFINE_INSTANCED_PROP(half, _Metallic)
        UNITY_DEFINE_INSTANCED_PROP(half, _Smoothness)
        UNITY_INSTANCING_BUFFER_END(Props)


        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            const fixed4 color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color) * IN.color;

            float fresnel = dot(IN.worldNormal, IN.viewDir);
            fresnel = pow(saturate(1 - fresnel), 4);
            
            o.Albedo = color.rgb;
            o.Alpha = color.a * (0.05 + 0.95 * fresnel);
            o.Metallic = UNITY_ACCESS_INSTANCED_PROP(Props, _Metallic);
            o.Smoothness = UNITY_ACCESS_INSTANCED_PROP(Props, _Smoothness);
            o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
        }
        ENDCG
    }
}