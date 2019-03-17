Shader "Custom/LitOceanShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

		_AmplitudeA ("Amplitude1", Float) = 1.0
		_WavelengthA ("Wavelength1", Float) = 0.5
		_SpeedA ("Speed1", Float) = 1.0
		_SteepnessA ("Steepness1", Range(0,1)) = 0.5
		_DirectionA ("Direction1", Vector) = (1,0,0,0)

		_AmplitudeB ("Amplitude2", Float) = 1.0
		_WavelengthB ("Wavelength2", Float) = 0.5
		_SpeedB ("Speed2", Float) = 1.0
		_SteepnessB ("Steepness2", Range(0,1)) = 0.5
		_DirectionB ("Direction2", Vector) = (1,0,0,0)

		_AmplitudeC ("Amplitude3", Float) = 1.0
		_WavelengthC ("Wavelength3", Float) = 0.5
		_SpeedC ("Speed3", Float) = 1.0
		_SteepnessC ("Steepness3", Range(0,1)) = 0.5
		_DirectionC ("Direction3", Vector) = (1,0,0,0)

		_AmplitudeD ("Amplitude4", Float) = 1.0
		_WavelengthD ("Wavelength4", Float) = 0.5
		_SpeedD ("Speed4", Float) = 1.0
		_SteepnessD ("Steepness4", Range(0,1)) = 0.5
		_DirectionD ("Direction4", Vector) = (1,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

		float _AmplitudeA, _AmplitudeB, _AmplitudeC, _AmplitudeD;
		float _WavelengthA, _WavelengthB, _WavelengthC, _WavelengthD;
		float _SpeedA, _SpeedB, _SpeedC, _SpeedD;
		float _SteepnessA, _SteepnessB, _SteepnessC, _SteepnessD;
		float4 _DirectionA, _DirectionB, _DirectionC, _DirectionD;

		float3 GerstnerWave (float a, float l, float s, float q, float3 d, float3 p, inout float3 tangent, inout float3 binormal)
		{
			float w = ((2 * UNITY_PI) / l);
			float2 dir = normalize(d.xy);
			float direction = dot(d.xy, p.xz);
			float phi = s*w;
			float steepness = q/(w*a*4);

			float3 wave = float3(
				steepness * a * dir.x * cos(w * direction + _Time.x * phi),
				a * sin(w * direction + _Time.x * phi),
				steepness * a * dir.y * cos(w * direction + _Time.x * phi)
			);

			/*normal += float3(
				d.x * w * a * cos(w * direction +_Time.x * phi),
				steepness * w * a * sin(w * direction + _Time.x * phi),
				d.y * w * a * cos(w * direction + _Time.x * phi)
			);*/

			tangent += float3(
				steepness * dir.x * dir.y * w * a * sin(w * direction + _Time.x * phi),
				dir.y * w * a * cos(w * direction + _Time.x * phi),
				steepness * pow(dir.y, 2) * w * a * sin(w * direction + _Time.x * phi)
			);

			binormal += float3(
				steepness * pow(dir.x, 2) * w * a * sin(w * direction + _Time.x * phi),
				dir.x * w * a * cos(w * direction + _Time.x * phi),
				steepness * dir.x * dir.y * w * a * sin(w * direction + _Time.x * phi)
			);

			return wave;
		}

		void vert (inout appdata_full v)
		{
			float3 currentPoint = v.vertex.xyz;
			float3 newPoint = 0;
			float3 tangent = 0;
			float3 binormal = 0;

			float4 Qs = float4(_SteepnessA, _SteepnessB, _SteepnessC, _SteepnessD);
			Qs = normalize(Qs);

			newPoint += GerstnerWave(_AmplitudeA, _WavelengthA, _SpeedA, Qs.x, _DirectionA, currentPoint, tangent, binormal);
			newPoint += GerstnerWave(_AmplitudeB, _WavelengthB, _SpeedB, Qs.y, _DirectionB, currentPoint, tangent, binormal);
			newPoint += GerstnerWave(_AmplitudeC, _WavelengthC, _SpeedC, Qs.z, _DirectionC, currentPoint, tangent, binormal);
			newPoint += GerstnerWave(_AmplitudeD, _WavelengthD, _SpeedD, Qs.w, _DirectionD, currentPoint, tangent, binormal);

			float3 normal = normalize(cross(float3(1.0 - binormal.x, binormal.y, -binormal.z), float3(-tangent.x, tangent.y, 1-tangent.z)));
			v.vertex.xyz = float3(currentPoint.x + newPoint.x, newPoint.y, currentPoint.z + newPoint.z);
			v.normal = normal;
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
