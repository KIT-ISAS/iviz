
Shader "iviz/MultiplyMesh"
{
    Properties
    {
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		[Toggle(USE_TEXTURE)] _UseTexture("Use Texture", Float) = 1
		[Toggle(USE_TEXTURE_SCALE)] _UseTextureScale("Use Texture ScaleY", Float) = 1
		[Toggle(USE_TEXTURE_SCALE_ALL)] _UseTextureScaleAll("Use Texture Scale All", Float) = 0
		_MainTex("Atlas Texture", 2D) = "defaulttexture" {}    	
    }
    SubShader
    {
        Tags {"RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard addshadow fullforwardshadows vertex:vert
        #pragma instancing_options procedural:setup 
		#pragma multi_compile _ USE_TEXTURE USE_TEXTURE_SCALE USE_TEXTURE_SCALE_ALL
        #pragma multi_compile_instancing
        #pragma target 4.5

        struct PointWithColor {
            float3 pos;
#if USE_TEXTURE || USE_TEXTURE_SCALE || USE_TEXTURE_SCALE_ALL
		    float intensity;
#else
			int intensity;
#endif
        };

        struct Input
        {
            fixed3 color;
			UNITY_VERTEX_OUTPUT_STEREO
        };

#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
        StructuredBuffer<PointWithColor> _Points;
#endif
		float _AtlasRow;
		sampler2D _MainTex;
        
        half _Glossiness;
        half _Metallic;
        float4 _LocalOffset;
        float4 _LocalScale;

		float _IntensityCoeff;
		float _IntensityAdd;

        float4x4 _LocalToWorld;
        float4x4 _WorldToLocal;
        float4 _BoundaryCenter;
        float4 _Tint;

        void setup() {}

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_SETUP_INSTANCE_ID(v);
            UNITY_INITIALIZE_OUTPUT(Input, o);

#ifdef USING_STEREO_MATRICES
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
#endif

        	v.normal = normalize(mul(v.normal, (float3x3)_WorldToLocal));
        	
#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            v.vertex *= _LocalScale;
            v.vertex += _LocalOffset;

        	uint instanceID = v.instanceID;

	#ifdef USING_STEREO_MATRICES
			instanceID /= 2;
	#endif


    #if USE_TEXTURE || USE_TEXTURE_SCALE || USE_TEXTURE_SCALE_ALL
			float intensity = _Points[instanceID].intensity;
    #endif

	#if USE_TEXTURE_SCALE
			v.vertex.y *= intensity;
    #endif
	#if USE_TEXTURE_SCALE_ALL
			v.vertex.xyz *= intensity;
    #endif

            v.vertex.xyz += _Points[instanceID].pos;
            v.vertex = mul(_LocalToWorld, v.vertex);
            v.vertex -= _BoundaryCenter;

        	

	#if USE_TEXTURE || USE_TEXTURE_SCALE || USE_TEXTURE_SCALE_ALL
			o.color = tex2Dlod(_MainTex, float4(intensity * _IntensityCoeff + _IntensityAdd, _AtlasRow, 0, 0));
    #else
            int color = _Points[instanceID].intensity;
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
        }
        ENDCG
    	
    }
}
