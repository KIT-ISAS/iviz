Shader "iviz/GridMap"
{
	SubShader
	{
		Tags { "RenderType"="Opaque"}
		LOD 200

        CGPROGRAM
        #pragma surface surf Standard addshadow fullforwardshadows vertex:vert
        #pragma target 3.0

        sampler2D _SquareTexture;
		sampler2D _InputTexture;
		sampler2D _IntensityTexture;

		float _IntensityCoeff;
		float _IntensityAdd;
		float4 _Tint;
					
		float _SquareCoeff;
		
		struct Input {
			float2 squareTextureUV;
			float intensity;
		};

		half _Smoothness;
		half _Metallic;

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            
            float2 uv = v.vertex.xy;
            float input = tex2Dlod(_InputTexture, float4(uv, 0, 0));
            v.vertex.y = input;
            o.intensity = input * _IntensityCoeff + _IntensityAdd;
			o.squareTextureUV = uv * _SquareCoeff;             
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

