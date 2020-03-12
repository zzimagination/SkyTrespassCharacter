// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/LineShader"
{
    Properties
    {
			_Color("Color",Color) = (1.0,1.0,1.0,1.0)
	        _EdgeColor("Edge Color",Color) = (1.0,1.0,1.0,1.0)
			_Width("Width",Range(0,1)) = 0.2
    }
    SubShader
    {
        Tags {	"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"  }
		LOD 200


        Pass
        {
			
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Front
			zWrite off
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
                float4 vertex : SV_POSITION;
            };

			fixed4 _Color;
			fixed4 _EdgeColor;
			float _Width;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col;
				float lx = step(_Width, i.uv.x);
				float ly = step(_Width, i.uv.y);
				float hx = step(i.uv.x, 1.0 - _Width);
				float hy = step(i.uv.y, 1.0 - _Width);
				col = lerp(_EdgeColor, _Color, lx*ly*hx*hy);
                return col;
            }
            ENDCG
        }
		Pass{
				Blend SrcAlpha OneMinusSrcAlpha
				LOD 200
				Cull Back
				zWrite off
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
				float4 vertex : SV_POSITION;
				};

				fixed4 _Color;
				fixed4 _EdgeColor;
				float _Width;

				v2f vert(appdata v) {
					v2f o;
					o.uv = v.uv;
					o.vertex = UnityObjectToClipPos(v.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target{
					fixed4 col;
				float lx = step(_Width, i.uv.x);

					float ly = step(_Width, i.uv.y);
					float hx = step(i.uv.x, 1.0 - _Width);
					float hy = step(i.uv.y, 1.0 - _Width);
					col = lerp(_EdgeColor, _Color, lx*ly*hx*hy);
					return col;
				}
					ENDCG
		}
    }
}
