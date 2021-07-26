Shader "iviz/DepthCloud"
{
	Properties
	{
		_PointSize("Point Size", Float) = 0.05
		_ColorTexture("Color Texture", 2D) = "white"
		_DepthTexture("Depth Texture", 2D) = "white"
		//_IntensityTexture("Intensity Texture", 2D) = "white"

		//_IntensityCoeff("Intensity Coeff", Float) = 1
		//_IntensityAdd("Intensity Add", Float) = 0
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

			float4 _Pos_ST;

			float4x4 _LocalToWorld;
			float4x4 _WorldToLocal;

			sampler2D _ColorTexture;
			sampler2D _DepthTexture;

			float _PointSize;
			float _DepthScale;

            static const float4 Quad[4] =
            {
                float4(-0.5, -0.5, 0, 0),
                float4(0.5, -0.5, 0, 0),
                float4(0.5, 0.5, 0, 0),
                float4(-0.5, 0.5, 0, 0),
            };

			StructuredBuffer<float2> _Points;

			struct appdata
			{
				uint id : SV_VertexID;
				uint inst : SV_InstanceID;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};			
			
			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};


			//v2f vert(uint id : SV_VertexID, uint inst : SV_InstanceID)
			v2f vert(appdata In)
			{
				unity_ObjectToWorld = _LocalToWorld;
				unity_WorldToObject = _WorldToLocal;

				const uint id = In.id;
				const uint inst = In.inst;

				v2f o;
#ifdef USING_STEREO_MATRICES
				UNITY_SETUP_INSTANCE_ID(In);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				inst /= 2;
#endif				

				const float2 extent = abs(float2(UNITY_MATRIX_P._11, UNITY_MATRIX_P._22));
				const float2 quadVertex = Quad[id];
				float2 center = _Points[inst];

				const float z = tex2Dlod(_DepthTexture, float4(center, 0, 0)).r * _DepthScale;
				const float2 size = z * extent * _PointSize;

				float4 pos;
				pos.xy = (center * _Pos_ST.xy + _Pos_ST.zw) * z;
				pos.y *= -1;
				pos.z = z;
				pos.w = 1;

				// Set vertex output.
				o.position = UnityObjectToClipPos(pos) + float4(quadVertex * size, 0, 0);				 
                o.uv = center;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return tex2D(_ColorTexture, i.uv);
			}

			ENDCG
		}
	}
}
