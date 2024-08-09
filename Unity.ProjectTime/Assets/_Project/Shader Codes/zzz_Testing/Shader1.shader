Shader "Unlit/Shader1"
{
    Properties //input Data
    {
        //_MainTex ("Texture", 2D) = "white" {}
        //_value ("Value", Float) = 1.0
        _colorA ("ColorA", Color) = (1,1,1,1)
        _colorB ("ColorB", Color) = (1,1,1,1)
        _colorStart ("ColorStart", Range(0, 1)) = 1
        _colorEnd ("ColorEnd", Range(0, 1)) = 0
        [Toggle] _Enable ("Enable ?", Float) = 0
        [Enum(Off, 0, Front, 1, Back, 2)]
        _Face ("Face Culling", Float) = 0
        [Space(100)]
        //[PowerSlider(3.0)] _PropertyName ("Display name", Range (0.01, 1)) = 0.08
        [IntRange] _PropertyName ("Display name", Range (0, 255)) = 100
    }
    SubShader
    {
        Tags 
            { 
                "RenderType"="Transparent"
                "Queue"="Transparent"
            }
        Pass
        {
            //pass tags
            Cull front
            ZWrite on
            //ZTest GEqual
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"
            #define TAU 6.283185307179586
            
            float4 _colorA;
            float4 _colorB;
            float _colorStart;
            float _colorEnd;

            struct MeshData // per-vertex mesh data
            {
                float4 vertex : POSITION; // vertex position
                float3 normals : NORMAL;
                float2 uv0 : TEXCOORD0; // uv coordinates
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD1; //clip space position
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD0;
            };


            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex); //local space to clip space
                o.normal = UnityObjectToWorldNormal(v.normals);
                //o.uv = (v.uv0 + _offSet) * _scale;
                o.uv = v.uv0;
                return o;
            }

            float InverseLerp(float a, float b, float v)
            {
                return (v-a)/(b-a);
            }

            float4 frag (Interpolators i) : SV_Target
            {
                // float4 outColor = lerp(_colorA, _colorB, i.uv.x);
                // return outColor;

                //float t = saturate(InverseLerp(_colorStart, _colorEnd, i.uv.x));
                //t = frac(t);
                //float t = abs(frac(i.uv.x * 2) * 2 - 1);
                float xOffset = cos(i.uv.x * TAU * 3) * .01;
                float t = cos((i.uv.y + xOffset - _Time.y * 0.27182818) * TAU * 1) * 0.5 + 0.9;
                t*=(1-i.uv.y);
                //return t * (abs(i.normal.y) < 0.999) ;
                float4 outColor = lerp(_colorA, _colorB, t);
                return outColor;
            }
            ENDCG
        }
    }
}
