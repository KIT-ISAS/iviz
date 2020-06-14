
Shader "iviz/MultiplyMeshOcclusionOnly"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Tags { "Queue" = "Geometry-1" }

        ZWrite On
        ZTest LEqual
        ColorMask 0
        Lighting Off

        CGPROGRAM
        #pragma surface surf Standard vertex:vert
        #pragma instancing_options procedural:setup 
		#pragma multi_compile _ USE_TEXTURE_SCALE
        #pragma multi_compile_instancing
        #pragma target 4.5

        struct PointWithColor {
            float3 pos;
#if USE_TEXTURE_SCALE
		    float intensity;
#else
			int dummy; // unused
#endif
        };

        struct Input
        {
            float dummy; // unused
        };

#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
        StructuredBuffer<PointWithColor> _Points;
#endif

        float4 _LocalOffset;
        float4 _LocalScale;

		sampler2D _IntensityTexture;
		float _IntensityCoeff;
		float _IntensityAdd;

        float4x4 _LocalToWorld;
        float4 _BoundaryCenter;

        void setup() {}

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_SETUP_INSTANCE_ID(v);
            UNITY_INITIALIZE_OUTPUT(Input, o);

#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            v.vertex *= _LocalScale;
            v.vertex += _LocalOffset;

	#if USE_TEXTURE_SCALE
			float intensity = _Points[v.instanceID].intensity;
			v.vertex.y *= intensity;
    #endif

            v.vertex.xyz += _Points[v.instanceID].pos;
            v.vertex = mul(_LocalToWorld, v.vertex);
            v.vertex -= _BoundaryCenter;
#endif
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = fixed4(0, 0, 0, 0);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
