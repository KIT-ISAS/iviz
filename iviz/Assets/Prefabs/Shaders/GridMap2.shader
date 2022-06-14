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

        #pragma multi_compile _ USE_NORMALS
        #pragma multi_compile_instancing

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

            float2 uv = v.texcoord;
            const float input = tex2Dlod(_InputTex, float4(uv, 0, 0));
            v.vertex.y += input;

#if USE_NORMALS
            const float2 normalCoeff = _SquareCoeff.xy;
            const float2 normalOffset = _SquareCoeff.zw;
            const float px = tex2Dlod(_InputTex, float4(uv.x + normalOffset.x, uv.y, 0, 0));
            const float py = tex2Dlod(_InputTex, float4(uv.x, uv.y + normalOffset.y, 0, 0));
            float3 f;
            f.x = (py - input) * normalCoeff.x;
            f.y = 1;
            f.z = -(px -  input) * normalCoeff.y;
            v.normal = normalize(f);
#endif

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
            //o.Albedo = float4(1,1,1,1);
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }

    FallBack "Standard"
}