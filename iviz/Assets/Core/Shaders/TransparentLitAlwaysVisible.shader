Shader "iviz/TransparentLitAlwaysVisible"
{
    Properties
    {
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent+1" "RenderType"="Transparent"
        }
        ZWrite On
        ZTest Always

        CGPROGRAM
        #pragma surface surf Standard alpha:fade
        #pragma target 3.0

        struct Input
        {
            float4 color : COLOR;
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
            o.Albedo = color.rgb;
            o.Alpha = color.a;
            o.Metallic = UNITY_ACCESS_INSTANCED_PROP(Props, _Metallic);
            o.Smoothness = UNITY_ACCESS_INSTANCED_PROP(Props, _Smoothness);
            o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
        }
        ENDCG

    }
}