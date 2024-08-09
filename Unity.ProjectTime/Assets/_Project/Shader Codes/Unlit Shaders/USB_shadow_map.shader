Shader "Unlit/USB_shadow_map"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		// shadow caster Pass
		Pass
		{
			Name "Shadow Caster"
			Tags
			 {
			 	"RenderType"="Opaque"
				"LightMode"="ShadowCaster"
			 }
			
			ZWrite On

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				// we need only the position of vertices as input
				float4 vertex : POSITION;
			};

			struct v2f
			{
				// we need only the position of vertices as output
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			 {
				return 0;
			 }
			
			ENDCG
		}
		
		// default color pass
		
		Pass
		{
			
		}
	}
}