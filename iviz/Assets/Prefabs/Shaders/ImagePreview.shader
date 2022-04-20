Shader "iviz/ImagePreview"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white"
		_IntensityTexture("Intensity Texture", 2D) = "white"

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15 

		_IntensityCoeff("Intensity Coeff", Float) = 1
		_IntensityAdd("Intensity Add", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}

		//Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ USE_INTENSITY FLIP_RB

			sampler2D _MainTex;
			sampler2D _IntensityTexture;
			float4 _MainTex_ST;
			float _IntensityCoeff;
			float _IntensityAdd;

			struct appdata_t
			{
				float4 position : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 position : POSITION;
				half4 color		: COLOR;
				float2 texcoord : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
			};

			v2f vert(appdata_t v)
			{
				v2f OUT;
				OUT.position = UnityObjectToClipPos(v.position);
				OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				OUT.color = v.color;
				OUT.worldPosition = v.position;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color;
#if USE_INTENSITY
				float i = tex2D(_MainTex, IN.texcoord).r * _IntensityCoeff + _IntensityAdd;
				color = tex2D(_IntensityTexture, float2(i, 0));
#elif FLIP_RB
				color = tex2D(_MainTex, IN.texcoord).zyxw;
#else
				color = tex2D(_MainTex, IN.texcoord);
#endif
				color *= IN.color;
				color.a = 1;

#ifdef UNITY_UI_CLIP_RECT
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
#endif

#ifdef UNITY_UI_ALPHACLIP
				clip(color.a - 0.001);
#endif

				return color;
			}

			ENDCG
		}
	}
}
