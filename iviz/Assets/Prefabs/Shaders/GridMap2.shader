Shader "iviz/GridMap"
{
    Properties
    {
        _IntensityTex("Atlas Texture", 2D) = "defaulttexture" {}
        _SquareTex("Square Texture", 2D) = "defaulttexture" {}
        _InputTex("Input Texture", 2D) = "defaulttexture" {}
    }

    SubShader
    {
        CGPROGRAM
        #pragma surface surf Standard vertex:vert addshadow fullforwardshadows

        float _AtlasRow;
        sampler2D _SquareTex;
        sampler2D _InputTex;
        sampler2D _IntensityTex;

        float _IntensityCoeff;
        float _IntensityAdd;
        float4 _Tint;
        float _Metallic;
        float _Smoothness;
        
        float4 _SquareCoeff;

        struct Input
        {
            float2 squareTextureUV : TEXCOORD0;
            float2 intensityUV : TEXCOORD1;
        };

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);

            float2 uv = v.vertex.xz; // ros transform
            uv = uv.yx * float2(-1, 1); // y positive is down
            const float input = tex2Dlod(_InputTex, float4(uv, 0, 0));
            v.vertex.y = input;
            o.intensityUV = float2(input * _IntensityCoeff + _IntensityAdd, _AtlasRow);
            o.squareTextureUV = uv * _SquareCoeff.xy;
        }

        //void surf(Input IN, inout SurfaceOutput o)
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo =
                tex2D(_IntensityTex, IN.intensityUV) *
                tex2D(_SquareTex, IN.squareTextureUV) *
                _Tint;
            //o.Emission = o.Albedo * 0;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }

    FallBack "Standard"
}