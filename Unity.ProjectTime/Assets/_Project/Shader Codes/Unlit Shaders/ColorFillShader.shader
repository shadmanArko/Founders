Shader "Unlit/ColorFillShader"
{
    Properties
    {
        _TotalWeeksPassed ("Total Weeks Passed", int) = 0
        _MovementCostInWeeks ("Movement Cost In Weeks", int) = 0
        _FilledColor ("Filled Color", Color) = (1, 0, 0, 1) // Default is solid red
        _UnfilledColor ("Unfilled Color", Color) = (1, 1, 1, 1) // Default is white
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }


        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed _TotalWeeksPassed;
            fixed _MovementCostInWeeks;
            fixed4 _FilledColor;
            fixed4 _UnfilledColor;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed fillAmount = _TotalWeeksPassed / _MovementCostInWeeks; // Normalize the slider value to 0-1 range
                fixed3 finalColor = lerp(_UnfilledColor.rgb, _FilledColor.rgb, step(i.uv.x, fillAmount));
                return fixed4(finalColor, 1);
           
            }
            ENDHLSL
        }
    }
}
