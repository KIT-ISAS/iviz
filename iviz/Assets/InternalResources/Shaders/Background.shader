Shader "iviz/Background"
{
    Properties
    {
        _Color0 ("Color 0", Color) = (1,1,1,1)
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (1,1,1,1)
        _Color3 ("Color 3", Color) = (1,1,1,1)
        _Color4 ("Color 4", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderQueue"="Geometry-1" }
        LOD 100

        Pass
        {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members u)
#pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float u : TEXCOORD_0;
            };

            fixed4 _Color0, _Color1, _Color2, _Color3, _Color4;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.u = asin(v.vertex.y) * 2 / 3.141592;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 cols[5] = { _Color0, _Color1, _Color2, _Color3, _Color4}; 
                
                float tu = (i.u+ 0.5) / 1 * 4;
                int p = min((int)tu, 3);
                //int p = (int)tu;
                float pp = min(tu - p, 1);
                
                
                fixed4 col = lerp(cols[p], cols[p+1], pp);
                return col;
            }
            ENDCG
        }
    }
}
