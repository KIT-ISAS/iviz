Shader "Ibis/HeightMap"
{
	Properties
	{
		_HeightTexture("Height Texture", 2D) = "white"
		_IntensityTexture("Intensity Texture", 2D) = "white"
		_ColorMapTexture("Colormap Texture", 2D) = "white"
		_MinIntensity("Min Intensity", float) = 0
		_InvSpanIntensity("InvSpan Intensity", float) = 1
	}
	
	SubShader
	{
		Pass
		{
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			// compile shader into multiple variants, with and without shadows
			// (we don't care about any lightmaps yet, so skip these variants)
			#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
			// shadow helper functions and macros
			#include "AutoLight.cginc"

			sampler2D _HeightTexture;
			sampler2D _IntensityTexture;
			sampler2D _ColorMapTexture;
			float _MinIntensity;
			float _InvSpanIntensity;

			struct v2f
			{
				SHADOW_COORDS(1) // put shadows data into TEXCOORD1
				fixed3 diff : COLOR0;
				fixed3 ambient : COLOR1;
				float4 pos : SV_POSITION;
				float intensity : COLOR2;
			};

			v2f vert(appdata_base v)
			{
				v2f o;

				float4 uv = float4(v.vertex.x, 1-v.vertex.y, 0, 0);
				float z = tex2Dlod(_HeightTexture, uv).r;
				float l = tex2Dlod(_IntensityTexture, uv).r;
				float4 pos = float4(v.vertex.xy, z, 1);
				o.pos = UnityObjectToClipPos(pos);
				o.intensity = (l - _MinIntensity) * _InvSpanIntensity;
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				o.diff = nl * _LightColor0.rgb;
				o.ambient = ShadeSH9(half4(worldNormal,1));
				// compute shadows data
				TRANSFER_SHADOW(o)
				return o;
			}


			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_ColorMapTexture, float2(i.intensity, 0));
				// compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
				fixed shadow = SHADOW_ATTENUATION(i);
				// darken light's illumination with shadow, keep ambient intact
				fixed3 lighting = i.diff * shadow + i.ambient;
				col.rgb *= lighting;
				return col;
			}
			ENDCG
		}

		// shadow casting support
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}