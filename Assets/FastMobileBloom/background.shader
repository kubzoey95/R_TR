Shader "Unlit/background"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
		Cull Off ZWrite Off //ZTest Always

        Pass
        {
            CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
#pragma exclude_renderers gles
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.vertex, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed uvOffset = 0;
				fixed scanLinePos = 1 - fmod(_Time[0], 0.2) / 0.2;
				uvOffset = 0.01 * float(i.uv.y - 0.1 < scanLinePos && i.uv.y + 0.1 > scanLinePos) * sin((i.uv.y + _Time[0]) * 1000);
				return fixed4(tex2D(_MainTex, i.uv + fixed2(uvOffset, 0)).x, tex2D(_MainTex, i.uv + fixed2(uvOffset, 0.01)).y, tex2D(_MainTex, i.uv + fixed2(uvOffset, 0.02)).z, 1);
            }
            ENDCG
        }
    }
}
