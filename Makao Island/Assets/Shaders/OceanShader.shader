Shader "Unlit/OceanShader"
{
    Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		[NoScaleOffset]_NoiseSample("NoiseSample", 2D) = "gray" {}
		_Wavelength("Wavelength", Float) = 10
		_Steepness("Steepness", Range(0, 1)) = 0.5
		_Speed("Speed", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float4 vertex : SV_POSITION;
            };

            sampler2D _NoiseSample;
			fixed4 _Color;
			float _Wavelength;
			float _Steepness;
			float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

				float n = tex2Dlod(_NoiseSample, float4(v.uv.xy, 0.0, 0.0));

				float k = 2 * UNITY_PI / _Wavelength;
				float a = _Steepness / k;
				float f = k * (o.vertex.x - _Speed * _Time.y);
				
				o.vertex.x += a * cos(f);
				o.vertex.y += a * sin(f);
			
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = _Color;
                return col;
            }
            ENDCG
        }
    }
}
