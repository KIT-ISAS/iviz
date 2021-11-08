Shader "iviz/LineMeshSimple"
{
    Properties
    {
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

        struct Input
        {
            int dummy;
        };

        fixed4 _Tint;
        
        half4 LightingNoLighting(SurfaceOutput s, half3 __, half ___)
        {
            half4 c;
            c.rgb = s.Albedo;
            c.a = 1;
            return c;
        }

        void surf(Input __, inout SurfaceOutput o)
        {
            o.Albedo = _Tint;
            o.Alpha = 1;
        }
        ENDCG
    }
}