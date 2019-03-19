Shader "Custom/RiverShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap ("Normal map", 2D) = "bump" {}
		_FlowMap ("Flow", 2D) = "black" {}
		
		_Tiling ("Tiling", Float) = 1
		_Speed ("Speed", Float) = 1
		//_UJump ("U jump per phase", Range(-0.25,0.25)) = 0.25
		//_VJump ("V jump per phase", Range(-0.25,0.25)) = 0.25

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _NormalMap;
		sampler2D _FlowMap;

		float _Tiling;
		float _Speed;

		//float _UJump;
		//float _VJump;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

		float2 FlowUV (float2 uv, float2 flow, float tiling, float time, out float2x2 rotation)
		{
			float2 dir = normalize(flow.xy);
			rotation = float2x2(dir.y, dir.x, -dir.x, dir.y);
			uv = mul(float2x2(dir.y, -dir.x, dir.x, dir.y), uv);
			uv.y -= time;

			return uv * tiling;
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 flow = (tex2D(_FlowMap, IN.uv_MainTex).rg * 2) - 1;
			float time = _Time.y * _Speed;
			float2x2 rotation;
			
			float2 uv = FlowUV(IN.uv_MainTex, flow, _Tiling, time, rotation);

			float3 normal = UnpackNormal(tex2D(_NormalMap, uv.xy));
			normal.xy = mul(rotation, normal.xy);
			o.Normal = normalize(normal);

            fixed4 c = tex2D(_MainTex, uv.xy) * _Color;
            o.Albedo = c.rgb;
			o.Alpha = c.a;

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
