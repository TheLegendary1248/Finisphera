Shader "Unlit/FloorShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
					float4 worldSpacePos : TEXCOORD1;
				};

				fixed4 _Color;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif
					OUT.worldSpacePos = mul(unity_ObjectToWorld, IN.vertex);
					return OUT;
				}

				sampler2D _MainTex;
				sampler2D _AlphaTex;
				float _AlphaSplitEnabled;

				fixed4 SampleSpriteTexture(float2 uv)
				{
					fixed4 color = tex2D(_MainTex, uv);

	#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
					if (_AlphaSplitEnabled)
						color.a = tex2D(_AlphaTex, uv).r;
	#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

					return color;
				}
				float negMod(float x, float N) 
				{
					return (x % N + N) % N;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
					c.rgb *= c.a;
					half4 col = half4(IN.worldSpacePos.xy, 1, 1);
					float alter = negMod(col.x + (negMod(col.x, 1) > 0.5? col.y : -col.y), 1) < 0.5;
					c.rgb *= alter ? 1 : 0.75 ;
					return c;
				/*
					fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
					c.rgb *= c.a;
					half4 col = half4(IN.worldSpacePos.xy, 1, 1);
					float segs = 16;
					float fracNum = (1 / segs);
					bool oh = ((negMod(col.x, fracNum * 2) * segs) - 1) > 0;
					bool shit = ((negMod(col.y, fracNum * 2) * segs) - 1) > 0;
					//clip((((col.x + col.y) % (fracNum * 2)* segs) - 1));
					col = (col % fracNum) * segs;
					bool crap = true;
					if (oh)
					{
						crap = (col.x > col.y) == shit;
					}
					else {
						crap = (col.x > 1 + -col.y) == shit;
					}
				
					
					return float4(crap,1,0,1);*/
				}
				
			ENDCG
			}
		}
}
