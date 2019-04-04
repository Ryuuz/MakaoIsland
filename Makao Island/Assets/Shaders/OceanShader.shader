Shader "Custom/OceanShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.0
		_Transparency ("Water transparency", Range(0, 1)) = 0.5
		
		_FogColor ("Water fog color", Color) = (0,0,0,0)
		_FogDensity ("Water density", Range(0,2)) = 0.1

		_FoamColor ("Foam color", Color) = (1, 1, 1, 1)
		_DepthFactor ("Depth factor", Float) = 1.0

		_Speed ("Speed", Range(0, 1)) = 0.4
		_Height ("Wave height", Range(0, 1)) = 0.5
		_Amount ("Amount", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

		Cull off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		GrabPass
		{
			"_WaterBackground"
		}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha vertex:vert finalcolor:ResetAlpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float4 screenPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float _Transparency;

		float3 _FogColor;
		float _FogDensity;

		float4 _FoamColor;
		float _DepthFactor;

		float _Speed;
		float _Height;
		float _Amount;
		sampler2D _CameraDepthTexture;
		sampler2D _WaterBackground;

		float3 UnderwaterVisuals(float depthDifference, float2 uv)
		{
			float3 backgroundColor = tex2D(_WaterBackground, uv).rgb;
			float fog = exp2((-_FogDensity) * depthDifference);
			return lerp(_FogColor, backgroundColor, fog);
		}

		void ResetAlpha(Input IN, SurfaceOutputStandard o, inout fixed4 color)
		{
			color.a = 1;
		}

        void vert(inout appdata_full v)
		{
			v.vertex.y += sin(_Time.z * _Speed + (v.vertex.x * v.vertex.z * _Amount)) * _Height;

			float3 tangent = normalize(float3(1, _Amount * _Height * cos(_Time.z * _Speed + (v.vertex.x * v.vertex.z * _Amount)), 0));
			v.normal = float3(-tangent.y, tangent.x, 0);
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.screenPos.xy / IN.screenPos.w;
			
			float backgroundDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
			float surfaceDepth = UNITY_Z_0_FAR_FROM_CLIPSPACE(IN.screenPos.z);
			float difference = backgroundDepth - surfaceDepth;

			float foamLine = 1 - saturate(_DepthFactor * (backgroundDepth - IN.screenPos.w));

            fixed4 c = _Color + (foamLine * _FoamColor);
            o.Albedo = c.rgb;

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Transparency;
			o.Emission = UnderwaterVisuals(difference, uv) * (1 - _Transparency);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
