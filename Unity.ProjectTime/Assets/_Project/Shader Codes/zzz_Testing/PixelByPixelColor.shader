Shader "Unlit/CustomGridShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Color;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 fragCoord = i.vertex.xy;
                float2 iResolution = _ScreenParams.xy;
                float scale = 50.0;

                float2 uv = scale * fragCoord / iResolution;

                float2 pixelUV = floor(uv); // Round down to the nearest pixel

                // Set the size of each pixel
                float pixelSize = 50.0 / scale;

                // Set the colors for the pixels
                fixed4 backgroundColor = float4(1.0, 1.0, 1.0, 1.0);
                fixed4 lineColor = float4(1.0, 0.0, 0.0, 1.0); // Red color for the line
                fixed4 pixelColor = backgroundColor; // Initialize pixel color as background color

                // Define the list of line pixels
                float2 linePixels[12];
                linePixels[0] = float2(10.0, 5.0);
                linePixels[1] = float2(11.0, 6.0);
                linePixels[2] = float2(14.0, 11.0);
                linePixels[3] = float2(15.0, 5.0);
                linePixels[4] = float2(16.0, 8.0);
                linePixels[5] = float2(17.0, 11.0);
                linePixels[6] = float2(18.0, 5.0);
                linePixels[7] = float2(19.0, 8.0);
                linePixels[8] = float2(20.0, 11.0);
                linePixels[9] = float2(21.0, 5.0);
                linePixels[10] = float2(22.0, 8.0);
                linePixels[11] = float2(23.0, 11.0);

                // Check if the fragment coordinate matches any of the line pixel coordinates
                for (int j = 0; j < 12; j++)
                {
                    if (pixelUV.x == linePixels[j].x && pixelUV.y == linePixels[j].y)
                    {
                        pixelColor = lineColor;
                        break; // Exit the loop if a matching pixel is found
                    }
                }

                return pixelColor * _Color;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}





//void mainImage(out vec4 fragColor, in vec2 fragCoord)
//{
//    float scale = 50.0;
//
//    vec2 uv = scale * fragCoord.xy / iResolution.xx;
//
//    vec2 pixelUV = floor(uv); // Round down to the nearest pixel
//
//    // Set the size of each pixel
//    float pixelSize = 50.0 / scale;
//
//    // Set the colors for the pixels
//    vec4 backgroundColor = vec4(1.0, 1.0, 1.0, 1.0);
//    vec4 lineColor = vec4(1.0, 0.0, 0.0, 1.0); // Red color for the line
//    vec4 pixelColor = backgroundColor; // Initialize pixel color as background color
//
//    // Define the list of line pixels
//    vec2 linePixels[12];
//    linePixels[0] = vec2(10.0, 5.0);
//    linePixels[1] = vec2(11.0, 6.0);
//    linePixels[2] = vec2(14.0, 11.0);
//    linePixels[3] = vec2(15.0, 5.0);
//    linePixels[4] = vec2(16.0, 8.0);
//    linePixels[5] = vec2(17.0, 11.0);
//    linePixels[6] = vec2(18.0, 5.0);
//    linePixels[7] = vec2(19.0, 8.0);
//    linePixels[8] = vec2(20.0, 11.0);
//    linePixels[9] = vec2(21.0, 5.0);
//    linePixels[10] = vec2(22.0, 8.0);
//    linePixels[11] = vec2(23.0, 11.0);
//
//    // Check if the fragment coordinate matches any of the line pixel coordinates
//    for (int i = 0; i < 12; i++)
//    {
//        if (pixelUV == linePixels[i])
//        {
//            pixelColor = lineColor;
//            break; // Exit the loop if a matching pixel is found
//        }
//    }
//
//    fragColor = pixelColor;
//}

