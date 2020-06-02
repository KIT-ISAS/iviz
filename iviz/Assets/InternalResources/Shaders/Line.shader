Shader "iviz/Line"
{
	Properties
	{
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
				half4 color : COLOR;
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
				half4 rgbaA = half4(
					(cA >>  0) & 0xff,
					(cA >>  8) & 0xff,
					(cA >> 16) & 0xff,
					(cA >> 24) & 0xff
					) / 255.0;
				int cB = _Lines[inst].colorB;
				half4 rgbaB = half4(
					(cB >>  0) & 0xff,
					(cB >>  8) & 0xff,
					(cB >> 16) & 0xff,
					(cB >> 24) & 0xff
					) / 255.0;
				o.color = (rgbaB - rgbaA) * V.z + rgbaA;
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				return i.color;
			}

			ENDCG
		}
	}
}
