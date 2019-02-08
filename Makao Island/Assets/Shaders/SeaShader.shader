Shader "Custom/SeaShader"
{
    Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		[NoScaleOffset]_MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_Steepness("Steepness", Range(0, 1)) = 0.5
		_Wavelength("Wavelength", Float) = 10
		_Speed("Speed", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float _Steepness;
		float _Wavelength;
		float _Speed;

		void vert(inout appdata_full v)
		{
			float3 p = v.vertex.xyz;

			float k = 2 * UNITY_PI / _Wavelength;
			float f = k * (p.x - _Speed * _Time.y);
			float a = _Steepness / k;
			float n = tex2Dlod(_MainTex, float4(v.texcoord.xy, 0, 0));


			p.y += a * cos(f);
			p.x += a * sin(f);

			

			float3 tangent = normalize(float3(1 - k * _Steepness * cos(f), k * _Steepness * sin(f), 0));
			float3 normal = float3(-tangent.y, tangent.x, 0);

			v.vertex.xyz = p;
			v.normal = normal;
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
