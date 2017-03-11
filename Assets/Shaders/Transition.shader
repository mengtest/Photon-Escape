Shader "Transition Shader"
{
	Properties
	{
		_MainTex("Texture", any) = "" {}
		_CPoint("Center Point",Vector) = (0,0,0,0)
		_TPoint("Target Point",Vector) = (0,0,0,0)
		_OFFSET("Cutoff",float) = 0
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

			float4 _CPoint;
			float4 _TPoint;
			sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			float _Completion;
			float alph;
			float4 retCol;
			float _OFFSET;

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
			float fullDistance = distance(_CPoint, _TPoint);
			float Completion = min(max(_Completion, 0), 1) * fullDistance;
			float calcDistance =Completion;
			_OFFSET = max(_Completion,0.0001)*10*_OFFSET +calcDistance;
			float pixelDistance = max(calcDistance, min(fullDistance, distance(_CPoint,i.vertex)));

				if (i.origColor.r > 0.9)
				{
					retCol = i.origColor;
				}else
				{
					retCol = tex2D(_MainTex, i.uv);
				}



				if (distance(_CPoint, i.vertex) > calcDistance)
				{
					alph =  1 - ((min(distance(_CPoint, i.vertex),_OFFSET) - calcDistance) / ( _OFFSET - calcDistance));
				}
				else
				{
					alph = ((pixelDistance - distance(_CPoint, i.vertex)) / (calcDistance - distance(_CPoint, i.vertex)));
				}
			
			retCol.a = min(min(tex2D(_MainTex, i.uv).a,i.origColor.a),alph);
                 return retCol;
            }
			ENDCG
		}
	}

}
