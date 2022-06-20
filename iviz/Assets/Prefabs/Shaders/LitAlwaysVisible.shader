Shader "iviz/LitAlwaysVisible"
{
	Properties
	{
		_Color("Diffuse Color", Color) = (1,1,1,1)
		_Smoothness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metalness", Range(0,1)) = 0.5
	}
	SubShader
	{
        //Pass 
        //{
		    //Tags { "Queue"="Transparent+1" "RenderType"="Transparent"}
			ZWrite On
    
            CGPROGRAM
            #pragma surface surf Standard
            #pragma target 3.0
    
            struct Input {
                float4 color : COLOR;
            };
    
            half _Smoothness;
            half _Metallic;
    
            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _EmissiveColor)
            UNITY_INSTANCING_BUFFER_END(Props)
    
            void surf(Input IN, inout SurfaceOutputStandard o) {
                o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb * IN.color;
            	o.Alpha = 1;
                o.Metallic = _Metallic;
                o.Smoothness = _Smoothness;
                o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
            }
            ENDCG

		
			ZWrite Off
            ZTest Greater
		
            CGPROGRAM
            #pragma surface surf Standard alpha:fade
            #pragma target 3.0
    
            struct Input {
                float4 color : COLOR;
            };
    
            half _Smoothness;
            half _Metallic;
    
            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _EmissiveColor)
            UNITY_INSTANCING_BUFFER_END(Props)
    
            void surf(Input IN, inout SurfaceOutputStandard o) {
                o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb * IN.color;
            	o.Alpha = 0.1;
                o.Metallic = _Metallic;
                o.Smoothness = _Smoothness;
                o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
            }
            ENDCG		
		
	}
}

