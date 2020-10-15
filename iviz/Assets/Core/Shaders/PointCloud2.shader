
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

			struct v2f
			{
				float4 position : SV_POSITION;
				fixed3 color : COLOR;
			};

			v2f vert(uint id : SV_VertexID, uint inst : SV_InstanceID)
			{
				unity_ObjectToWorld = _LocalToWorld;
				unity_WorldToObject = _WorldToLocal;

				float2 extent = abs(float2(UNITY_MATRIX_P._11, UNITY_MATRIX_P._22)) * _Scale;
				float4 quadVertex = Quad[id] * float4(extent, 1, 1);
				Point centerPoint = _Points[inst];
				float4 centerVertex = float4(centerPoint.position, 1);

				v2f o;
				o.position = UnityObjectToClipPos(centerVertex) + quadVertex;
#ifdef USE_TEXTURE
				float centerIntensity = centerPoint.intensity;
				o.color = tex2Dlod(_AtlasTexture, float4(centerIntensity * _IntensityCoeff + _IntensityAdd, _AtlasRow, 0, 0));
#else
				int centerIntensity = centerPoint.intensity;
				fixed3 rgb = fixed3(
					(centerIntensity >>  0) & 0xff,
					(centerIntensity >>  8) & 0xff,
					(centerIntensity >> 16) & 0xff
					) / 255.0;
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
