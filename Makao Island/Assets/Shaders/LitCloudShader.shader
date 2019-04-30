Shader "Custom/LitCloudShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SecondTex ("Texture", 2D) = "white" {}
		_Cutoff ("Cloud cutoff", Range(0,0.99)) = 0.2
		_Softness ("Cloud softness", Range(0,3)) = 0.3
		[HideInInspector]_MidYValue ("Middle Y value", Float) = 1.0
		[HideInInspector]_Height ("Cloud Height", Float) = 1.0
		_Taper ("Taper amount", Range(0,2)) = 1.26
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		fixed4 _Color;
        sampler2D _MainTex;
		sampler2D _SecondTex;
		float _Cutoff;
		float _Softness;
		float _MidYValue;
		float _Height;
		float _Taper;
		half _Glossiness;
        half _Metallic;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

		//https://www.patreon.com/posts/volumetric-write-21646034
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 mainColor = tex2D(_MainTex, IN.uv_MainTex + (_Time.y * 0.005));
			fixed4 noiseColor = tex2D(_SecondTex, (IN.uv_MainTex - (_Time.y * 0.005)) * 0.5);

			float falloff = abs(_MidYValue - IN.worldPos.y) / (_Height * 0.5);
			falloff = saturate(falloff);
			falloff = pow(falloff, _Taper);
			falloff = 1.0 - falloff;

			fixed4 finalColor = mainColor * noiseColor * falloff;

			finalColor = 0.0 + (finalColor - _Cutoff) * (1.0 - 0.0) / (1.0 - _Cutoff);
			finalColor = saturate(finalColor);
			finalColor = pow(finalColor, _Softness);

			fixed4 glow = mainColor * noiseColor * falloff;
			glow = falloff - glow;
			glow = 1.0 - glow;

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
			o.Emission = glow * _Color;

			o.Albedo = finalColor.rgb * _Color.rgb;
            o.Alpha = saturate(finalColor.r * 3.0);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
