Shader "Unlit/Uv_Scroll"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

		_XShift("Xuv Shift", Range(-1.0, 1.0)) = 0.1
		_XSpeed("X Scroll Speed", Range(1.0, 100.0)) = 10.0
    
		_Color("Color"       , Color) = (1, 1, 1, 1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness"  , Range(0, 1)) = 0.5
		_Metallic("Metallic"    , Range(0, 1)) = 0.0
		_Cutoff("Cutoff"      , Range(0, 1)) = 0.5
	}
    SubShader
    {
		Tags{
			"Queue" = "AlphaTest"
			"RenderType" = "TransparentCutout"
		}

		LOD 200

		Cull Off

		CGPROGRAM
			#pragma target 3.0
			#pragma surface surf Standard fullforwardshadows alphatest:_Cutoff

		fixed4 _Color;
		sampler2D _MainTex;
		half _Glossiness;
		half _Metallic;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			float _XShift;
			float _XSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

				//speed
				_XShift = _XShift * _XSpeed;

				//add Shift
				i.uv.x = i.uv.x + _XShift * _Time;

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
	FallBack "Transparent/Cutout/Diffuse"
}
