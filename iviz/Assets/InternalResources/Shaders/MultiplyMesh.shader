
Shader "iviz/MultiplyMesh"
{
    Properties
    {
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags {"RenderType"="Opaque" }
        //Tags {"Queue" = "Transparent" "RenderType"="Transparent"  }
		//Blend SrcAlpha OneMinusSrcAlpha
		LOD 200

        CGPROGRAM
        #pragma surface surf Standard addshadow fullforwardshadows vertex:vert
        //#pragma surface surf Standard vertex:vert alpha
        #pragma instancing_options procedural:setup 
		#pragma multi_compile _ USE_TEXTURE USE_TEXTURE_SCALE
        #pragma multi_compile_instancing
        #pragma target 4.5

        struct PointWithColor {
            float3 pos;
#if USE_TEXTURE || USE_TEXTURE_SCALE
		    float intensity;
#else
			int intensity;
#endif
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
        float4 _LocalOffset;
        float4 _LocalScale;

		sampler2D _IntensityTexture;
		float _IntensityCoeff;
		float _IntensityAdd;

        float4x4 _LocalToWorld;
        float4 _BoundaryCenter;
        float4 _Tint;

        void setup() {}

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_SETUP_INSTANCE_ID(v);
            UNITY_INITIALIZE_OUTPUT(Input, o);

#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            v.vertex *= _LocalScale;
            v.vertex += _LocalOffset;

	#if USE_TEXTURE || USE_TEXTURE_SCALE
			float intensity = _Points[v.instanceID].intensity;
    #endif

	#if USE_TEXTURE_SCALE
			v.vertex.y *= intensity;
    #endif

            v.vertex.xyz += _Points[v.instanceID].pos;
            v.vertex = mul(_LocalToWorld, v.vertex);
            v.vertex -= _BoundaryCenter;

	#if USE_TEXTURE || USE_TEXTURE_SCALE
			o.color = tex2Dlod(_IntensityTexture, float4(intensity * _IntensityCoeff + _IntensityAdd, 0, 0, 0));
    #else
            int color = _Points[v.instanceID].intensity;
            o.color = float3(
                (color >> 0) & 0xff,
                (color >> 8) & 0xff,
                (color >> 16) & 0xff
                ) / 255.0;
    #endif
#endif
            o.color *= _Tint;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = IN.color;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            //o.Alpha = 0.4f;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
