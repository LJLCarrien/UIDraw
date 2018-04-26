Shader "Carrien/Board"
{
	Properties
	{
		_MainTex("Texture", 2D) = "black" {}
		_SubTex("Texture", 2D) = "black" {}
		_IsReset("IsReset",Float)=0

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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half4 _MainTex_TexelSize;

			sampler2D _SubTex;
			float4 _SubTex_ST;
			float _IsReset;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;

			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;

			};
			
			v2f o;
			v2f vert(appdata v)
			{
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord= TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color;
								
				return o;
			}


			half4 frag(v2f i) : SV_Target
			{
			fixed4 subCol=_IsReset*tex2D(_MainTex, i.texcoord)+(1-_IsReset)*tex2D(_SubTex,i.texcoord);
			//fixed4 handelCol = abs(sign(subCol.a)-(1-subCol));
			//fixed4 finalCol=handelCol*i.color;
			fixed4 finalCol=subCol*i.color;

			return finalCol;
			}
			ENDCG
		}
	}

}


