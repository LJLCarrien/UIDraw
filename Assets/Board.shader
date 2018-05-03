Shader "Carrien/Board"
{
	Properties
	{
		_MainTex("Texture", 2D) = "black" {}
		_SubTex("Texture", 2D) = "black" {}
		_IsReset("IsReset",Float) =0
	}
		SubShader
		{
			Pass
			{
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					half4 color:COLOR;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					half4 color:COLOR;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				half4 _MainTex_TexelSize;
				sampler2D _SubTex;
				float _IsReset;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.color = v.color;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{

				fixed4 subCol = _IsReset*tex2D(_MainTex, i.uv)+(1- _IsReset)*tex2D(_SubTex, i.uv);
				fixed4 handelCol = abs(sign(subCol.a) - (1 - subCol));
				fixed4 finalCol = handelCol;
				finalCol.a = subCol.a;
				finalCol *= i.color;
				return finalCol;
				}
				ENDCG
			}
		}
}
