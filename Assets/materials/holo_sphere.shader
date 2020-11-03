Shader "Unlit/holoSphere"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_ScannerSpeed("Scanner Speed", Float) = 4
		BoundRadius("Bounding Circle Radius", Float) = 1
		BoundCenter("Bound Center", Vector) = (0,0,0)
		Brightness("Brightness", Float) = 1
		AlphaMult("Alpha Multiplier", Float) = 1
		Tint("Tint Multiplier", Float) = 1
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
			#pragma multi_compile_instancing
            #include "UnityCG.cginc"
			#pragma instancing_options procedural:setup

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float howFar : TEXCOORD1;
				float depth : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
            };

			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_INSTANCING_BUFFER_END(Props)

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _ScannerSpeed;
			float BoundRadius;
			float3 BoundCenter;
			float Brightness;
			float AlphaMult;
			float Tint;
			float DistortionPow;
			float DistortionSpeed;

            v2f vert (appdata v)
            {
                v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				o.howFar = length(v.vertex.xyz - BoundCenter) / BoundRadius;
				o.howFar *= o.howFar;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.vertex.x += sin(_Time[0] * DistortionSpeed + o.vertex.y) * DistortionPow;
				fixed4 screenPos = ComputeScreenPos(o.vertex);
				screenPos /= screenPos.w;
				o.depth = screenPos.z;
                o.uv = TRANSFORM_TEX(screenPos, _MainTex);
				o.uv.y += _Time[0] * _ScannerSpeed;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				col.xyz /= Brightness;
				float farth = i.howFar * i.howFar;
				col.w *= 1 - (i.howFar);
				farth *= farth * farth;
				col.xyz *= float3(farth, farth, farth) * Tint;
				col.w /= AlphaMult;
                return col;
            }
            ENDCG
        }
    }
}
