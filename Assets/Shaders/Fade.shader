Shader "Fade Shader"
{
	Properties
	{
		_MainTex("Texture", any) = "" {}
		_Completion("Completion",float) = 0
	}

		SubShader

		{ 
			Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			ZWrite Off
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag



				#include "UnityCG.cginc"

			sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			float _Completion;
			float alph;
			float4 retCol;

			struct appdata{
			float2 uv :TEXCOORD;
			float4 vertex:POSITION;
			float4 origColor:COLOR;
			};

            appdata vert(appdata v){
            appdata o;
		     o.vertex = mul (UNITY_MATRIX_MVP, v.vertex);
            o.origColor = v.origColor;
			 o.uv = v.uv;
            return o;
            }
            float4 frag(appdata i) : COLOR {
			alph = _Completion;
			retCol.a = min(tex2D(_MainTex, i.uv).a,alph);
                 return retCol;
            }
			ENDCG
		}
	}

}
