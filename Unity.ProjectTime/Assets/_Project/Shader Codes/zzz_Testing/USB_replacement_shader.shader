Shader "Unlit/USB_replacement_shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
//        _FrontTexture ("FrontTexture", 2D) = "white" {}
//        _BackTexture ("BackTexture", 2D) = "white" {}
//        [Enum(UnityEngine.Rendering.BlendMode)]
//        _SrcBlend ("SrcFactor", Float) = 1
//        [Enum(UnityEngine.Rendering.BlendMode)]
//        _DstBlend ("DstFactor", Float) = 1
//        [Enum(UnityEngine.Rendering.CullMode)]
//        _Cull ("Cull", Float) = 0
    }
    SubShader
    {
//        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
////        AlphaToMask On
//        Blend [_SrcBlend] [_DstBlend]
////        ColorMask RGB
//        
//        Cull [_Cull]
//        
//        Blend SrcAlpha OneMinusSrcAlpha
//        ZWrite on             
        
//        ZTest LEqual
        //LOD 100
        
        Tags { "Queue"="Geometry-1" }
                ZWrite Off
                ColorMask 0
        Stencil
            {
                Ref 2 // StencilRef
                Comp Always
                Pass Replace
            }

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

            sampler2D _MainTex;
            // sampler2D _FrontTexture;
            // sampler2D _BackTexture;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i, bool face : SV_IsFrontFace) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // fixed4 colFront = tex2D(_FrontTexture, i.uv);
                // fixed4 colBack = tex2D(_BackTexture, i.uv);
                // return face ? colFront : colBack;

                // apply red color
                fixed4 red = fixed4(1, 1, 0, 1);
                return col ;
            }
            ENDCG
        }
    }
}
