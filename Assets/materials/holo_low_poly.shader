Shader "Unlit/holo"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_ScannerSpeed("Scanner Speed", Float) = 4
		BoundRadius("Bounding Circle Radius", Float) = 1
		BoundCenter("Bound Center", Vector) = (0,0,0)
		Brightness("Brightness", Float) = 1
		DistortionPow("Distortion Power", Float) = 0
		DistortionSpeed("Distortion Speed", Float) = 5
    }
    SubShader
    {
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Cull front
		LOD 100

        Pass
        {
            CGPROGRAM
			#pragma vertex vert alpha
			#pragma fragment frag alpha
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float depth : TEXCOORD2;
				float4 realPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _ScannerSpeed;
			float BoundRadius;
			float3 BoundCenter;
			float Brightness;
			float DistortionPow;
			float DistortionSpeed;

            v2f vert (appdata v)
            {
                v2f o;

				o.realPos = v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.vertex.x += sin(_Time[0] * DistortionSpeed + o.vertex.y) * DistortionPow;
				fixed4 screenPos = ComputeScreenPos(o.vertex);
				screenPos /= screenPos.w;
				o.depth = screenPos.z;
                o.uv = TRANSFORM_TEX(screenPos, _MainTex);
				o.uv.y += _Time[0] * _ScannerSpeed;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                UNITY_APPLY_FOG(i.fogCoord, col);
				float howFar = length(i.realPos.xyz - BoundCenter.xyz) / BoundRadius;
				col.xyz *= howFar * howFar * Brightness;
                return col;
            }
            ENDCG
        }
    }
}
