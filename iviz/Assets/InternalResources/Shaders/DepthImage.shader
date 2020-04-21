Shader "Ibis/DepthImage"
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
			//float4 _TexCoordOffset;
			sampler2D _ColorTexture;
			sampler2D _DepthTexture;

			StructuredBuffer<float2> _Quad;
			StructuredBuffer<float2> _Points;

			struct v2f
			{
				float4 position : SV_POSITION;
				half3 color : COLOR;
			};


			v2f vert(uint id : SV_VertexID, uint inst : SV_InstanceID)
			{
				unity_ObjectToWorld = _LocalToWorld;
				unity_WorldToObject = _WorldToLocal;

				float2 extent = abs(UNITY_MATRIX_P._11_22);
				float2 quadVertex = _Quad[id];
				float2 center = _Points[inst];

				float z = tex2Dlod(_DepthTexture, float4(center, 0, 0)).r * 65.535;
				float2 size = extent * z * _PointSize;

				float4 pos;
				pos.xy = (center * _Pos_ST.xy + _Pos_ST.zw) * z;
				pos.y *= -1;
				pos.z = z;
				pos.w = 1;

				// Set vertex output.
				v2f o;
				o.position = UnityObjectToClipPos(pos) + float4(quadVertex * size, 0, 0);
#if USE_INTENSITY
				o.color = tex2Dlod(_IntensityTexture, float4(z * _IntensityCoeff + _IntensityAdd, 0, 0, 0));
#else
				o.color = tex2Dlod(_ColorTexture, float4(center, 0, 0));
#endif
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
