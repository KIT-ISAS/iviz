Shader "iviz/GridMap"
{
	SubShader
	{
		Tags { "RenderType"="Opaque"}
		LOD 200

        CGPROGRAM
        #pragma surface surf Standard addshadow noforwardadd vertex:vert
        #pragma target 3.0

        sampler2D _SquareTexture;
		sampler2D _InputTexture;
		sampler2D _IntensityTexture;

		float _IntensityCoeff;
		float _IntensityAdd;
		float4 _Tint;
					
		float4 _SquareCoeff;
		
		struct Input {
			float2 squareTextureUV;
			float intensity;
		};

		half _Smoothness;
		half _Metallic;

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            
            float2 uv = v.vertex.xz * float2(1, 1); // ros transform
            uv = uv.yx * float2(-1, 1); // row major
            //uv = uv * float2(1, -1); // flipped transform
            //float2 uv = float2(v.vertex.x, -v.vertex.z);
            float input = tex2Dlod(_InputTexture, float4(uv, 0, 0));
            v.vertex.y = input;

/*
            float2 du = float2(_SquareCoeff.z, 0);
            float2 dv = float2(0, _SquareCoeff.w);
            float input_pu = tex2Dlod(_InputTexture, float4(uv + du, 0, 0));
            float input_mu = tex2Dlod(_InputTexture, float4(uv - du, 0, 0));
            float input_pv = tex2Dlod(_InputTexture, float4(uv + dv, 0, 0));
            float input_mv = tex2Dlod(_InputTexture, float4(uv - dv, 0, 0));
            float3 n = normalize(float3(-(input_pu - input_mu) / 2 * _SquareCoeff.x, 1, -(input_pv - input_mv) / 2 * _SquareCoeff.y));
            v.normal = n;
 */
            v.normal = float3(0, 1, 0);
 
            o.intensity = input * _IntensityCoeff + _IntensityAdd;
			o.squareTextureUV = uv * _SquareCoeff.xy;             
        }

		void surf(Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = 
			    tex2D(_IntensityTexture, IN.intensity) *
			    tex2D (_SquareTexture, IN.squareTextureUV).rgb * 
			    _Tint.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
		}
		ENDCG
	}
	FallBack "Standard"
}

