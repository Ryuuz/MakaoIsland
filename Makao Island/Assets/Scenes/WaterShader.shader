Shader "Unlit/WaterShader"
{
    Properties
    {
		_WaveSpeed ("Wave Speed", range(0.0, 1.0)) = 0.5
		_WaterColor ("Water Color", Color) = (0, 0, 1, 1)
    }
    SubShader
    {
        Tags
		{
			"RenderType"="Opaque"
		}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct vertIn
            {
                float4 vertex : POSITION;
				fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
            };

            float _WaveSpeed;
			fixed4 _WaterColor;

            v2f vert (vertIn v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;

				float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.vertex.y += sin(_Time*worldPos.x*_WaveSpeed)*1.5;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _WaterColor;
            }
            ENDCG
        }
    }
}
