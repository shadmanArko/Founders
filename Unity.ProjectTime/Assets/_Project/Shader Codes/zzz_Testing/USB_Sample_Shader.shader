Shader "USB/USB_simple_color"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Texture Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
//        Tags { "RenderType"="Transparent" "Queue"="Transparent" } // for Transparent
//        Blend SrcAlpha OneMinusSrcAlpha // for Transparent
        
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
              
                fixed4 col = tex2D(_MainTex, i.uv);
               
                return col*_Color;
            }
            ENDCG
        }
    }
}
