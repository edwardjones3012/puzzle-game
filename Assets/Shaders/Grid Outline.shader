Shader "Unlit/Grid Outline"
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
                float2 inverseUV = float2(1,1) - i.uv;
                float minDistance = min(inverseUV.x, inverseUV.y);
                float4 col = float4(0,1,0,1);
                return float4(minDistance.xx, 0, 1);
            }
            ENDCG
        }
    }
}

