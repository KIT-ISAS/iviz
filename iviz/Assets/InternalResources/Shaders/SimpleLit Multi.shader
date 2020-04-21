Shader "iviz/SimpleLit Multi"
{
	Properties
	{
		_Color("Diffuse Color", Color) = (1,1,1,1)
		_SpecColor("Specular Color", Color) = (1,1,1,1)
		_Specular("Specular", Float) = 0.5
		_Gloss("Gloss", Float) = 0.5
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 150

		CGPROGRAM
		#pragma surface surf BlinnPhong

		half4 _Color;
		half _Specular;
		half _Gloss;

		struct Input {
			half4 color : COLOR;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color.rgb * IN.color.rgb;
			o.Specular = _Specular;
			o.Gloss = _Gloss;
		}
		ENDCG
	}
}

