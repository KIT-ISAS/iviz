Shader "iviz/LinePulseSimple"
{
    Properties {}
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "LightMode"="Always"
        }

        CGPROGRAM
        #pragma surface surf NoLighting noforwardadd noambient alpha:fade

        struct Input
        {
            float3 worldPos;
        };

        fixed4 _Color;
        fixed4 _Tint;
        float4 _PulseCenter;
        float _PulseTime;
        float _PulseDelta;

        half4 LightingNoLighting(SurfaceOutput s, half3 __, half ___)
        {
            half4 c;
            c.rgb = s.Albedo;
            c.a = s.Alpha;
            //c.rgb = lerp(float3(1, 1, 1), float3(1, 0, 0), s.Alpha);
            //c.a = 1;
            return c;
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            const float3 diff = IN.worldPos - _PulseCenter;
            const float x = (length(diff) - _PulseTime) / _PulseDelta;
            const float alpha = exp(-x * x);
            o.Albedo = _Tint;
            o.Alpha = alpha;
        }
        ENDCG
    }
}