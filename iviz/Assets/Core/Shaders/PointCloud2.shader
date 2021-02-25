Shader "iviz/PointCloud2"
{
	Properties
	{
		[Toggle(USE_TEXTURE)] _UseTexture("Use Texture", Float) = 0		
		_AtlasTexture("Atlas Texture", 2D) = "defaulttexture" {}
	}

	SubShader
	{
		Cull Off
		Tags { "RenderType"="Opaque" }
		
		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ USE_TEXTURE

			float _IntensityCoeff;
			float _IntensityAdd;
			float4x4 _LocalToWorld;
			float4x4 _WorldToLocal;
	        fixed4 _Tint;
            float _Scale;
            
			struct Point {
				float3 position;
#if USE_TEXTURE
				float intensity;
#else
				int intensity;
#endif
			};

            static const float4 Quad[4] =
            {
                float4(-0.5, -0.5, 0, 0),
                float4(0.5, -0.5, 0, 0),
                float4(0.5, 0.5, 0, 0),
                float4(-0.5, 0.5, 0, 0),
            };
			
			StructuredBuffer<Point> _Points;
			float _AtlasRow;
			sampler2D _AtlasTexture;

			struct appdata
			{
				uint id : SV_VertexID;
				uint inst : SV_InstanceID;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 position : SV_POSITION;
				fixed3 color : COLOR;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			v2f vert(const appdata In)
			{
				unity_ObjectToWorld = _LocalToWorld;
				unity_WorldToObject = _WorldToLocal;

				uint id = In.id;
				uint inst = In.inst;

				v2f o;
#ifdef USING_STEREO_MATRICES
				UNITY_SETUP_INSTANCE_ID(In);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				inst /= 2;
#endif
				const float2 extent = abs(float2(UNITY_MATRIX_P._11, UNITY_MATRIX_P._22)) * _Scale;
				const float4 quad_vertex = Quad[id] * float4(extent, 1, 1);
				const Point center_point = _Points[inst];
				const float4 center_vertex = float4(center_point.position, 1);

				o.position = UnityObjectToClipPos(center_vertex) + quad_vertex;
#ifdef USE_TEXTURE
				const float center_intensity = center_point.intensity;
				o.color = tex2Dlod(_AtlasTexture, float4(center_intensity * _IntensityCoeff + _IntensityAdd, _AtlasRow, 0, 0));
#else
				const int center_intensity = center_point.intensity;
				const int3 rgbi = (center_intensity >> int3(0, 8, 16)) & 255;
				const float3 rgb = float3(rgbi) / 255; 
				o.color = rgb;
#endif
				o.color *= _Tint;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return fixed4(i.color, 1);
			}

			ENDCG
		}
	}
}

