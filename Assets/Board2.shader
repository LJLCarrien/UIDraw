Shader "Carrien/Board"
{
	Properties
	{
		_MainTex("Texture", 2D) = "black" {}
		_SubTex("Texture", 2D) = "white" {}
		_ArcAlpha("ArcAlpha",Float)=0
		_IsReset("IsReset",Float)=0

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
			float _ArcAlpha;
			float _IsReset;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;

			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;

			};
			
			v2f o;
			v2f vert(appdata v)
			{
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord= TRANSFORM_TEX(v.texcoord, _MainTex);
								
				return o;
			}


			half4 frag(v2f i) : SV_Target
			{
			fixed4 subCol=_IsReset*tex2D(_MainTex, i.texcoord)+(1-_IsReset)*tex2D(_SubTex,i.texcoord);
			fixed4 handelCol = abs(sign(subCol.a)-(1-subCol));
			fixed4 finalCol=handelCol;
			finalCol.a=subCol.a-_ArcAlpha;
			return finalCol;
			}
			ENDCG
		}
	}

}


