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
		    Tags { "Queue"="Transparent" "RenderType"="Transparent"}
            //Tags {"RenderType"="Opaque"}
    
            CGPROGRAM
            #pragma surface surf Standard addshadow fullforwardshadows
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
                o.Metallic = _Metallic;
                o.Smoothness = _Smoothness;
                o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
            }
            ENDCG
        //}
        
        //Pass 
        //{
		    //Tags { "Queue"="Transparent" "RenderType"="Transparent"}
		    ZWrite Off
            ZTest Greater
            Blend One SrcAlpha
    
            CGPROGRAM
            #pragma surface surf NoLighting alpha:fade
            #pragma target 3.0
    
            struct Input {
                float4 color : COLOR;
            };
    
            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _EmissiveColor)
            UNITY_INSTANCING_BUFFER_END(Props)
    
            fixed4 lightingNoLighting(SurfaceOutput s)
            {
                fixed4 c;
                c.rgb = s.Albedo; 
                c.a = s.Alpha;
                return c;
            }
    
            void surf(Input IN, inout SurfaceOutput o) {
                o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb * IN.color;
                o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _EmissiveColor).rgb;
                o.Alpha = 0.05;
            }
            ENDCG
        //}        
	}
	FallBack "Standard"
}

