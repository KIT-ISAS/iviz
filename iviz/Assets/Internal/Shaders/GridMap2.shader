Shader "iviz/GridMap"
{
    Properties
    {
        _IntensityTex("Atlas Texture", 2D) = "defaulttexture" {}
        _SquareTex("Square Texture", 2D) = "defaulttexture" {}
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        CGPROGRAM
        #pragma surface surf NoLighting noforwardadd noambient vertex:vert

        float _AtlasRow;
        sampler2D _SquareTex;
        sampler2D _InputTex;
        sampler2D _IntensityTex;

        float _IntensityCoeff;
        float _IntensityAdd;
        float4 _Tint;

        float4 _SquareCoeff;

        struct Input
        {
            float2 squareTextureUV : TEXCOORD0;
            float2 intensityUV : TEXCOORD1;
        };

        fixed4 LightingNoLighting(SurfaceOutput s, half3 _, half __)
        {
            fixed4 c;
            c.rgb = s.Albedo;
            c.a = 1;
            return c;
        }

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);

            float2 uv = v.vertex.xz * float2(1, 1); // ros transform
            uv = uv.yx * float2(-1, 1); // row major
            float input = tex2Dlod(_InputTex, float4(uv, 0, 0));
            v.vertex.y = input;
            o.intensityUV = float2(input * _IntensityCoeff + _IntensityAdd, _AtlasRow);
            o.squareTextureUV = uv * _SquareCoeff.xy;
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo =
                tex2D(_IntensityTex, IN.intensityUV) *
                tex2D(_SquareTex, IN.squareTextureUV) *
                _Tint;
        }
        ENDCG
    }
}