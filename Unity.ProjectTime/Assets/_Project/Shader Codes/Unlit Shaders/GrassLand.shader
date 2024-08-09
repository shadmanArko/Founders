Shader "Map/Grassland"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WorldScale("WorldScale", Float) = 0.25
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" "SHADOW_CASTER"="On" "FALLBACK"="OFF"}
        
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
            float4 _MainTex_ST;
            float _WorldScale;

            v2f vert (appdata v)
            {

                v2f o;

                 // Calculate UV coordinates based on vertex index and mesh-specific properties
                float2 uvOffset = float2(0.0, 0.0); // Offset values for UV coordinates
                float uvScale = 1.0; // Scale factor for UV coordinates

                // o.uv = float2(v.vertex.x, v.vertex.z);
                uvOffset *= _WorldScale;
                uvScale /= _WorldScale;
                o.uv = (float2(v.vertex.x, v.vertex.z) + uvOffset) * uvScale;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;

                
            }
            ENDCG
            
        }
    }
    Fallback "Diffuse"
}
