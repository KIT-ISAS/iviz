Shader "iviz/PointCloudDirect"
{
    Properties
    {
        [Toggle(USE_TEXTURE)] _UseTexture("Use Texture", Float) = 0
        _AtlasTexture("Atlas Texture", 2D) = "defaulttexture" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ USE_TEXTURE

            #include <UnityCG.cginc>

            float _IntensityCoeff;
            float _IntensityAdd;
            fixed4 _Tint;
            float _Scale;

            float _AtlasRow;
            sampler2D _AtlasTexture;

            struct appdata
            {
                float4 pos : POSITION;
#ifdef USE_TEXTURE
                float2 uv : TEXCOORD0;
#else
                fixed3 color : COLOR;
#endif
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR;
                float size : PSIZE;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert(const appdata In)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(In);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

#ifdef USE_TEXTURE
				o.color = tex2Dlod(_AtlasTexture, float4(In.uv.x * _IntensityCoeff + _IntensityAdd, _AtlasRow, 0, 0));
#else
                o.color = In.color;
#endif
                o.pos = UnityObjectToClipPos(In.pos);

                o.color *= _Tint;
                o.size = max(_Scale * 100, 1);
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