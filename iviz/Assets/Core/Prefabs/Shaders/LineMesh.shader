Shader "iviz/LineMesh"
{
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
			

			float4x4 _LocalToWorld;
			float4x4 _WorldToLocal;
			
			float _Scale;

            static const float3 Quads[8] =
            {
                float3(0.5, 0, 1),
                float3(0, 0.5, 1),
                float3(0, 0.5, 0),
                float3(-0.5, 0 , 0),

                float3(-0.5, 0 , 0),
                float3(0, -0.5, 0),
                float3(0, -0.5, 1),
                float3(0.5, 0, 1),
            };

			struct Line {
				float4 A;
				float4 B;
			};
			
			StructuredBuffer<Line> _Lines;

			struct appdata
			{
				uint id : SV_VertexID;
				uint inst : SV_InstanceID;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 position : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			v2f vert(const appdata In)
			{
				unity_ObjectToWorld = _LocalToWorld;
				unity_WorldToObject = _WorldToLocal;

				const uint id = In.id;
				uint inst = In.inst;
				
				v2f o;
#ifdef USING_STEREO_MATRICES
				UNITY_SETUP_INSTANCE_ID(In);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				inst /= 2;
#endif
				
				const float3 camPos = mul(_WorldToLocal, UNITY_MATRIX_IT_MV[3]).xyz;
				
				float3 v = Quads[id];
				v.xy *= _Scale;
				
				const float3 a = _Lines[inst].A;
				const float3 b = _Lines[inst].B;
				const float3 ba = b - a;
                const float3 right = normalize(ba);

				const float3 mid = (b + a) / 2;
				const float3 front = mid - camPos;
                const float3 up = normalize(cross(front, right));

                const float3 p = right * v.x + up * v.y + ba * v.z + a;
 
				o.position = UnityObjectToClipPos(p);
				return o;
			}

			fixed4 frag() : SV_Target
			{
				return float4(1, 1, 1, 1);
			}

			ENDCG
		}
	}
}
