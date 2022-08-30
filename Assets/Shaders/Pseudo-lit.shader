Shader "Unlit/Pseudo-lit"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _LightIntensity("Light Intensity", Range(0,1)) = 1
        _AmbientLight("Ambient Light", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _Color;
            float _LightIntensity;
            float _AmbientLight;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = v.normal;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 worldNormal = UnityObjectToWorldNormal(i.normal);
                
                float extraLight = worldNormal.y * _LightIntensity;
                float light = clamp(_AmbientLight + extraLight, 0, 1);
                float4 col = tex2D(_MainTex, i.uv) * _Color;
                float4 output = float4(col.xyz * light, 1);
                return output;
            }
            ENDCG
        }
    }
}
