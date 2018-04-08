Shader "Carrien/Board"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_SubTex("Texture", 2D) = "white" {}

	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }

		Pass
		{
			CGPROGRAM
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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half4 _MainTex_TexelSize;

			sampler2D _SubTex;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				#if UNITY_UV_STARTS_AT_TOP
				//	   if (_MainTex_TexelSize.y > 0)
				//	   o.uv.y = 1 - o.uv.y;
				//	   if (_MainTex_TexelSize.x > 0)
				//		   o.uv.x = 1 - o.uv.x;
				#endif

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{

			//	fixed4 col = tex2D(_MainTex, i.uv);
			//fixed4 subCol = tex2D(_SubTex, i.uv);
			//fixed4 finalCol;
			//if (!subCol.r > 0 || !subCol.g > 0 || !subCol.b > 0)
			//{
			//	subCol.r = col.r;
			//	subCol.g = col.g;
			//	subCol.b = col.b;
			//}
			//finalCol = col*subCol;
			//		return finalCol;

			fixed4 col = tex2D(_MainTex, i.uv);
			fixed4 subCol = 1-tex2D(_SubTex, i.uv);
			fixed4 finalCol;
			finalCol = col*subCol;
			return finalCol;
			}
			ENDCG
		}
	}
}
