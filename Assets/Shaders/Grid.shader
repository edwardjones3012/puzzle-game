Shader "Unlit/Grid"
{
    Properties
    {
        // _MainTex ("Texture", 2D) = "white" {}
        [IntRange] _Rows("Rows", Range(0, 100)) = 1
        [IntRange] _Columns("Columns", Range(0, 100)) = 1
        _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _Rows;
            float _Columns;
            fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 rUVs = float2(i.uv.x * _Rows, i.uv.y * _Columns);

                float xCloseToUpperInt = ceil(rUVs.x + .05) > ceil(rUVs.x);
                float xCloseToLowerInt = ceil(rUVs.x - .05) < ceil (rUVs.x);

                float yCloseToUpperInt = ceil(rUVs.y + .05) > ceil(rUVs.y);
                float yCloseToLowerInt = ceil(rUVs.y - .05) < ceil(rUVs.y);

                float isBorderRight = rUVs.x < .1;
                float isBorderTop = rUVs.y < .1;
                float isBorderLeft = rUVs.x > _Columns - .1;
                float isBorderBottom = rUVs.y > _Columns - .1;

                float withinGridLine = xCloseToUpperInt + xCloseToLowerInt + yCloseToUpperInt + yCloseToLowerInt
                    + isBorderRight + isBorderTop + isBorderLeft + isBorderBottom;

                withinGridLine = clamp(withinGridLine, 0, 1);
                return float4(withinGridLine.xxx * _Color, withinGridLine);

                float4 col = float4(0,1,0,1);
                return col;
            }
            ENDCG
        }
    }
}

