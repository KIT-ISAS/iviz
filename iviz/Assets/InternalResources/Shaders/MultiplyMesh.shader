
Shader "iviz/MultiplyMesh"
{
    Properties
    {
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _LocalScale("Local Scale", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard addshadow fullforwardshadows vertex:vert
        #pragma instancing_options procedural:setup 
        #pragma target 3.0

        struct PointWithColor {
            float3 pos;
            int color;
        };

        struct Input
        {
            half3 color;
        };

#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
        StructuredBuffer<PointWithColor> _Points;
#endif

        half _Glossiness;
        half _Metallic;
        float4 _LocalScale;

        float4x4 _LocalToWorld;
        float4 _BoundaryCenter;

        void setup() {}

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_SETUP_INSTANCE_ID(v);
            UNITY_INITIALIZE_OUTPUT(Input, o);

            v.vertex.xyz *= _LocalScale.xyz;
#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            v.vertex.xyz += _Points[v.instanceID].pos;
            v.vertex = mul(_LocalToWorld, v.vertex);
            v.vertex.xyz -= _BoundaryCenter;

            int color = _Points[v.instanceID].color;
            o.color = float3(
                (color >> 0) & 0xff,
                (color >> 8) & 0xff,
                (color >> 16) & 0xff
                ) / 255.0;
#endif
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = IN.color;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
