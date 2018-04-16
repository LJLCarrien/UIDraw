Shader "Carrien/Board"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_SubTex("Texture", 2D) = "white" {}
		_ReverseRange("ReverseRange",Range(-1,1))=1

	}
		SubShader
	{
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
				half4 color : COLOR;

			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				half4 color : COLOR;
				float2 uv : TEXCOORD0;
				float2 worldPos : TEXCOORD1;

			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half4 _MainTex_TexelSize;
		    fixed _ReverseRange;
			sampler2D _SubTex;

			float4 _ClipRange0 = float4(0.0, 0.0, 1.0, 1.0);
			float2 _ClipArgs0 = float2(1000.0, 1000.0);
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = v.vertex.xy * _ClipRange0.zw + _ClipRange0.xy;
				return o;
			}
			

			fixed4 frag(v2f i) : SV_Target
			{

			//fixed4 col = tex2D(_MainTex, i.uv);
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

			//fixed4 col = tex2D(_MainTex, i.uv);
			//fixed _ReverseFore=_ReverseRange*0.5+0.5;
			//fixed4 subCol = 1*_ReverseFore+(-1)*_ReverseRange*tex2D(_SubTex, i.uv);
			//fixed4 finalCol;
			//finalCol = col*subCol;
			//return finalCol;

			
			//fixed4 col = tex2D(_MainTex, i.uv);
			//fixed4 subCol=tex2D(_SubTex,i.uv);
			//fixed4 handelCol=abs(sign(subCol.a)-(1-subCol));
			//fixed4 finalCol=col*handelCol;
			//return finalCol;


			fixed4 col = tex2D(_MainTex, i.uv);
			fixed4 subCol=tex2D(_SubTex,i.uv);
			fixed4 handelCol=abs(sign(subCol.a)-(1-subCol));
			fixed4 finalCol=col*handelCol;

			float2 factor = (float2(1.0, 1.0) - abs(i.worldPos)) * _ClipArgs0;

			half4 cc=finalCol*i.color;
			cc.a*= clamp( min(factor.x, factor.y), 0.0, 1.0);
			return cc;
			}
			ENDCG
		}
	}
}
