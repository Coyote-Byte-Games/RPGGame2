Shader "Custom/CircularFillShaderURP" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _FillPercentage ("Fill Percentage", Range(0, 1)) = 0.5
    }

    SubShader {
        Tags {"Queue" = "Overlay" }
        Pass {
            Stencil {
                Ref 1
                Comp always
                Pass replace
            }
        }

        Pass {
            Tags { "LightMode" = "Overlay" }

            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            #pragma target 3.0
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : POSITION;
            };

            sampler2D _MainTex;
            float _FillPercentage;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                // Calculate the distance from the center of the sprite
                float2 center = float2(0.5, 0.5);
                float distance = distance(i.uv, center);

                // Discard pixels outside the specified fill percentage
                if (distance > _FillPercentage)
                    discard;

                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                return col;
            }
            ENDCG
        }
    }
}
