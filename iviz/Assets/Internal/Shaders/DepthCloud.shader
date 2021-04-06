Shader "iviz/DepthCloud"
{
	Properties
	{
		_PointSize("Point Size", Float) = 0.05
		_ColorTexture("Color Texture", 2D) = "white"
		_DepthTexture("Depth Texture", 2D) = "white"
		_IntensityTexture("Intensity Texture", 2D) = "white"

		_IntensityCoeff("Intensity Coeff", Float) = 1
		_IntensityAdd("Intensity Add", Float) = 0
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
			#pragma multi_compile _ USE_INTENSITY
			#pragma multi_compile_instancing

			sampler2D _IntensityTexture;
			float _IntensityCoeff;
			float _IntensityAdd;

			float4 _Pos_ST;

			float4x4 _LocalToWorld;
			float4x4 _WorldToLocal;

			float _PointSize;
			sampler2D _ColorTexture;
			sampler2D _DepthTexture;

			float _Scale;

			StructuredBuffer<float2> _Quad;
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

				uint id = In.id;
				uint inst = In.inst;

				v2f o;
#ifdef USING_STEREO_MATRICES
				UNITY_SETUP_INSTANCE_ID(In);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				inst /= 2;
#endif				

				float2 extent = abs(float2(UNITY_MATRIX_P._11, UNITY_MATRIX_P._22));
				float2 quadVertex = _Quad[id];
				float2 center = _Points[inst];

				float z = tex2Dlod(_DepthTexture, float4(center, 0, 0)).r * 65.535;
				float2 size = z * extent * _PointSize;

				float4 pos;
				pos.xy = (center * _Pos_ST.xy + _Pos_ST.zw) * z;
				pos.y *= -1;
				pos.z = z;
				pos.w = 1;

				// Set vertex output.
				o.position = UnityObjectToClipPos(pos) + float4(quadVertex * size, 0, 0);
				 
				/*
#if USE_INTENSITY
				o.color = tex2Dlod(_IntensityTexture, float4(z * _IntensityCoeff + _IntensityAdd, 0, 0, 0));
#else
				o.color = tex2Dlod(_ColorTexture, float4(center, 0, 0));
#endif
*/
#if USE_INTENSITY
                o.uv = float2(z * _IntensityCoeff + _IntensityAdd, 0);
#else
                o.uv = float2(center);
#endif
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
#if USE_INTENSITY
				return tex2D(_IntensityTexture, i.uv);
#else
				return tex2D(_ColorTexture, i.uv);
#endif
			}

			ENDCG
		}
	}
}
