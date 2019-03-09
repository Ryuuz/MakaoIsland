Shader "Unlit/CloudShader"
{
    Properties
    {
		_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
		_SecondTex ("Texture", 2D) = "white" {}
		_Cutoff ("Cloud cutoff", Range(0,1)) = 0.2
		_Softness ("Cloud softness", Range(0,3)) = 0.3
		[HideInInspector]_MidYValue ("Middle Y value", Float) = 1.0
		[HideInInspector]_Height ("Cloud Height", Float) = 1.0
		_Taper ("Taper amount", Float) = 1.26
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
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
				float4 worldPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

			fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _SecondTex;
			float _Cutoff;
			float _Softness;
			float _MidYValue;
			float _Height;
			float _Taper;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv + (_Time.y * 0.005));
				fixed4 noise = tex2D(_SecondTex, (i.uv - (_Time.y * 0.005)) * 0.5);

				float falloff = abs(_MidYValue - i.worldPos.y) / (_Height * 0.38);
				falloff = saturate(falloff);
				falloff = pow(falloff, _Taper);
				falloff = 1.0 - falloff;

				fixed4 colour = col * noise * falloff;

				colour = 0.0 + (colour - _Cutoff) * (1.0 - 0.0) / (1.0 - _Cutoff);
				colour = saturate(colour);
				colour = pow(colour, _Softness);

				colour.rgb = colour.rgb * _Color.rgb;
				colour.a = saturate(colour.r * 3.0);

				return colour;
            }
            ENDCG
        }
    }
}
