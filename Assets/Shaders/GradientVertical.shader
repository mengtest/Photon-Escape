Shader "Custom/GradientVertical" {
Properties {
	[PreRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
    _Color ("Bottom Color", Color) = (0.1,0.1,0.1,0.1)
    _Color2 ("Top Color", Color) = (0.1,0.1,0.1,0.1)
}
 
SubShader {
    Tags {"Queue"="Background"  "IgnoreProjector"="True"}
    LOD 100
 
    ZWrite On
 
    Pass {
        CGPROGRAM
        #pragma vertex vert  
        #pragma fragment frag
        #include "UnityCG.cginc"
 
        float4 _Color;
        float4 _Color2;
 
        struct v2f {
            float4 pos : SV_POSITION;
            float4 col : COLOR;
        };
 
        v2f vert (appdata_full v)
        {
            v2f o;
            o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
            o.col = lerp(_Color,_Color2, v.texcoord.y+0.2);
           //o.col = half4( v.vertex.x/2, 0, 0, 1);
            return o;
        }
       
 
        float4 frag (v2f i) : SV_Target {
            float4 c = i.col;
            c.a = 1;
            return c;
        }
            ENDCG
        }
    }
}