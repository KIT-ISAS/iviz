Shader "iviz/TransparentLine"
{
	Properties
	{
		[Toggle(USE_TEXTURE)] _UseTexture("Use Texture", Float) = 1		
		_MainTex ("Atlas Texture", 2D) = "defaulttexture" {}
	}

	SubShader
	{
		Cull Off
		Tags { "Queue" = "Transparent" "RenderType"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha

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

            static const float3 Quads[8] =
            {
                float3(0.5, 0, 1),
                float3(0, 0.5, 1),
                float3(0, 0.5, 0),
                float3(-0.5, 0 , 0),

                float3(0.5, 0, 1),
                float3(-0.5, 0 , 0),
                float3(0, -0.5, 0),
                float3(0, -0.5, 1),
            };

			struct Line {
				float3 A;
#if USE_TEXTURE
				float intensityA;
#else
				uint colorA;
#endif
				float3 B;
#if USE_TEXTURE
				float intensityB;
#else
				uint colorB;
#endif
			};

			StructuredBuffer<Line> _Lines;
			float _AtlasRow;
			sampler2D _MainTex;
			
			struct v2f
			{
				float4 position : SV_POSITION;
#if USE_TEXTURE
				float intensity : TEXCOORD;
#else
				fixed4 color : COLOR;
#endif
			};

			v2f vert(uint id : SV_VertexID, uint inst : SV_InstanceID)
			{
				unity_ObjectToWorld = _LocalToWorld;
				unity_WorldToObject = _WorldToLocal;

				float3 camPos = mul(_WorldToLocal, UNITY_MATRIX_IT_MV[3]).xyz;				
				
				float3 V = Quads[id];
				V.xy *= _Scale;
				
				float3 A = _Lines[inst].A;
				float3 B = _Lines[inst].B;
				float3 BA = B - A;
                float3 right = normalize(BA);

				float3 mid = (B + A) / 2;
				float3 front = mid - camPos;
                float3 up = normalize(cross(front, right));

                float3 p = right * V.x + up * V.y + BA * V.z + A;

				v2f o;
				o.position = UnityObjectToClipPos(float4(p, 1));

	#if USE_TEXTURE
				float intensityA = _Lines[inst].intensityA;
				float intensityB = _Lines[inst].intensityB;
				o.intensity = (intensityB - intensityA) * V.z + intensityA;
				o.intensity = o.intensity * _IntensityCoeff + _IntensityAdd;
    #else
				uint cA = _Lines[inst].colorA;
				fixed4 rgbaA = fixed4(
					(cA >>  0) & 0xff,
					(cA >>  8) & 0xff,
					(cA >> 16) & 0xff,
					(cA >> 24) & 0xff
					) / 255.0;
				uint cB = _Lines[inst].colorB;
				fixed4 rgbaB = fixed4(
					(cB >>  0) & 0xff,
					(cB >>  8) & 0xff,
					(cB >> 16) & 0xff,
					(cB >> 24) & 0xff
					) / 255.0;

				fixed4 diffuse = (rgbaB - rgbaA) * V.z + rgbaA;
                o.color = diffuse * _Tint;				
	#endif
				return o;
			}

			fixed4 frag(v2f o) : SV_Target
			{
	#if USE_TEXTURE
				return tex2D(_MainTex, float2(o.intensity, _AtlasRow)) * _Tint;
    #else
				return o.color;
	#endif
			}

			ENDCG
		}
	}
}
