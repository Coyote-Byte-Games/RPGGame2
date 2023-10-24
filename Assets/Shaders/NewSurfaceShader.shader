Shader "Custom/LoadingBarMask"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _MaskTex ("Mask (A)", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _MaskTex;

            float4 _MainTex_ST;
            float _FillAmount;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 mainColor = tex2D(_MainTex, i.texcoord);
                half4 maskColor = tex2D(_MaskTex, i.texcoord);
                
                // Apply masking based on fill amount
                half alpha = mainColor.a * maskColor.a;
                
                return half4(mainColor.rgb, alpha);
            }
            ENDCG
        }
    }
}
