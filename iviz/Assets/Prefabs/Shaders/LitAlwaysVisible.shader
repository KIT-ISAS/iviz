Shader "iviz/LitAlwaysVisible"
{
    Properties
    {
        _Alpha("Alpha", Range(0,1)) = 1
    }
    SubShader
    {
        ZWrite On

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows addshadow
        #pragma target 3.0

        struct Input
        {
            fixed4 color : COLOR;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _EmissiveColor)
        UNITY_DEFINE_INSTANCED_PROP(half, _Metallic)
        UNITY_DEFINE_INSTANCED_PROP(half, _Smoothness)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb * IN.color;
            o.Alpha = 1;
            o.Metallic = UNITY_ACCESS_INSTANCED_PROP(Props, _Metallic);
            o.Smoothness = UNITY_ACCESS_INSTANCED_PROP(Props, _Smoothness);
            o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
        }
        ENDCG


        ZWrite Off
        ZTest Greater

        CGPROGRAM
        #pragma surface surf Standard alpha:fade
        #pragma target 3.0

        struct Input
        {
            fixed4 color : COLOR;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _EmissiveColor)
        UNITY_DEFINE_INSTANCED_PROP(half, _Metallic)
        UNITY_DEFINE_INSTANCED_PROP(half, _Smoothness)
        UNITY_INSTANCING_BUFFER_END(Props)

        fixed _Alpha;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb * IN.color;
            o.Alpha = _Alpha;
            o.Metallic = UNITY_ACCESS_INSTANCED_PROP(Props, _Metallic);
            o.Smoothness = UNITY_ACCESS_INSTANCED_PROP(Props, _Smoothness);
            o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
        }
        ENDCG

    }
}