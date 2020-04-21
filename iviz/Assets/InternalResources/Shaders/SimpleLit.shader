Shader "iviz/SimpleLit"
{
	Properties
	{
		 _Color("Diffuse Color", Color) = (1,1,1,1)
		 _EmissiveColor("Emissive Color", Color) = (0,0,0,0)
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert noforwardadd

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
		UNITY_DEFINE_INSTANCED_PROP(fixed4, _EmissiveColor)
		UNITY_INSTANCING_BUFFER_END(Props)

		struct Input {
			half4 color : COLOR;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb;
			o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
		}
		ENDCG
	}
}

