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
            float2 squareTextureUV;
            float intensity;
        };

        half4 LightingNoLighting(SurfaceOutput s, half3 _, half __)
        {
            half4 c;
            c.rgb = s.Albedo;
            c.a = 1;
            return c;
        }

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);

            float2 uv = v.vertex.xz * float2(1, 1); // ros transform
            uv = uv.yx * float2(-1, 1); // row major
            //uv = uv * float2(1, -1); // flipped transform
            //float2 uv = float2(v.vertex.x, -v.vertex.z);
            float input = tex2Dlod(_InputTex, float4(uv, 0, 0));
            v.vertex.y = input;
            v.normal = float3(0, 1, 0);

            o.intensity = input * _IntensityCoeff + _IntensityAdd;
            o.squareTextureUV = uv * _SquareCoeff.xy;
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo =
                tex2D(_IntensityTex, float2(IN.intensity, _AtlasRow)) *
                tex2D(_SquareTex, IN.squareTextureUV).rgb *
                _Tint.rgb;
        }
        ENDCG
    }
}