Shader "iviz/Line"
{
	Properties
	{
	}

	SubShader
	{
		Cull Off

		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing

			float4x4 _LocalToWorld;
			float4x4 _WorldToLocal;
			float4 _Front;

			struct Line {
				float3 A;
                int colorA;
				float3 B;
				int colorB;
			};

			StructuredBuffer<float3> _Quad;
			StructuredBuffer<Line> _Lines;

			struct v2f
			{
				float4 position : SV_POSITION;
				half3 color : COLOR;
			};

			v2f vert(uint id : SV_VertexID, uint inst : SV_InstanceID)
			{
				unity_ObjectToWorld = _LocalToWorld;
				unity_WorldToObject = _WorldToLocal;

				float3 V = _Quad[id];
				float3 A = _Lines[inst].A;
				float3 B = _Lines[inst].B;
				float3 BA = B - A;
                float3 right = normalize(BA);
                float3 up = normalize(cross(_Front, right));

                float3 p = right * V.x + up * V.y + BA * V.z + A;

				v2f o;
				o.position = UnityObjectToClipPos(float4(p, 1));

				int cA = _Lines[inst].colorA;
				int cB = _Lines[inst].colorB;
				int c = (cB - cA) * V.z + cA;
				half3 rgb = half3(
					(c >>  0) & 0xff,
					(c >>  8) & 0xff,
					(c >> 16) & 0xff
					) / 255.0;
				o.color = rgb;
				//o.color = half3(1, 1, 1);
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				return half4(i.color, 1);
			}

			ENDCG
		}
	}
}
