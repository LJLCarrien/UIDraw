Shader "Carrien/Board"
{
	Properties
	{
		_MainTex("Texture", 2D) = "black" {}
		_SubTex("Texture", 2D) = "white" {}
		//_ReverseRange("ReverseRange",Range(-1,1))=1

	}
		SubShader
	{
		Pass
		{
		
			//ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha  

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half4 _MainTex_TexelSize;

		    //fixed _ReverseRange;
			sampler2D _SubTex;
			float4 _SubTex_ST;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float2 uv2_texcoord1 : TEXCOORD1;

			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				//float2 texcoord[3] : TEXCOORD0;
				float2 texcoord : TEXCOORD0;
				float2 uv2_texcoord1 : TEXCOORD1;

			};
			
			v2f o;
			v2f vert(appdata v)
			{
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord= TRANSFORM_TEX(v.texcoord, _MainTex);

				//o.texcoord[0]= TRANSFORM_TEX(v.texcoord, _MainTex);
				//o.texcoord[1]= ComputeScreenPos(o.vertex).xy;;
				//o.texcoord[2]= ComputeScreenPos(o.vertex).zw;
				
				o.uv2_texcoord1 = TRANSFORM_TEX(v.uv2_texcoord1, _SubTex);

				return o;
			}
			fixed4 circle(float2 pos,float2 circlePoint,float radius)
		{
			float d=length(pos-circlePoint)-radius;
			fixed4 col;
			if(d<radius)
			{
				col= fixed4(0,0,0,1);
			}
			else
			{
				col= fixed4(0,0,0,0);
			}
			

			return col;
		}


			half4 frag(v2f i) : SV_Target
			{

			//fixed4 col = tex2D(_MainTex, i.texcoord);
			//fixed4 subCol = tex2D(_SubTex, i.texcoord);
			//fixed4 finalCol;
			//if (!subCol.r > 0 || !subCol.g > 0 || !subCol.b > 0)
			//{
			//	subCol.r = col.r;
			//	subCol.g = col.g;
			//	subCol.b = col.b;
			//}
			//finalCol = col*subCol;
			//		return finalCol;

			//fixed4 col = tex2D(_MainTex, i.texcoord);
			//fixed _ReverseFore=_ReverseRange*0.5+0.5;
			//fixed4 subCol = 1*_ReverseFore+(-1)*_ReverseRange*tex2D(_SubTex, i.texcoord);
			//fixed4 finalCol;
			//finalCol = col*subCol;
			//return finalCol;

			
			//fixed4 col = tex2D(_MainTex, i.texcoord);
			//fixed4 subCol=tex2D(_SubTex,i.texcoord);
			//fixed4 handelCol=abs(sign(subCol.a)-(1-subCol));
			//fixed4 finalCol=col*handelCol;
			//return finalCol;


			//fixed4 col = tex2D(_MainTex, i.texcoord);
			//fixed4 subCol=tex2D(_SubTex,i.texcoord);
			//fixed4 handelCol=abs(sign(subCol.a)-(1-subCol));
			//fixed4 finalCol=col*handelCol;
			
			fixed4 col = tex2D(_MainTex, i.texcoord);
			fixed4 subCol=tex2D(_SubTex,i.uv2_texcoord1);
			fixed4 finalCol=fixed4(col.rgb*subCol.rgb,subCol.r);
			//finalCol.a = 0;
			return finalCol;
			}
			ENDCG
		}
	}

}

			//float2 fragCoord = (i.texcoord[1] / i.texcoord[2].y)*_ScreenParams.xy;
			//col.a=sin(fragCoord.x);

