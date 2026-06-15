Shader "Custom/UI_Flash_Final"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _Angle ("倾斜角度", Range(1, 89)) = 80
        _Width ("流光宽度", Range(0.1, 1)) = 0.25
        _Interval ("间隔时间", Float) = 3.0
        _Duration ("扫光时长", Float) = 1.5
        _FlowColor ("流光颜色", Color) = (1,1,1,1)
        _FlowIntensity ("发光强度", Range(0.1, 3)) = 1.5
        _RainbowSpeed ("彩虹速度", Range(0.5, 10)) = 2.0
        _RainbowMode ("开启炫彩彩虹 (1=开,0=关)", Float) = 1

        _StencilComp ("Stencil Comparison", Float) = 8
        _StencilID ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            float _Angle;
            float _Width;
            float _Interval;
            float _Duration;
            float3 _FlowColor;
            float _FlowIntensity;
            float _RainbowSpeed;
            float _RainbowMode;

            float3 Rainbow(float t)
            {
                float3 c = cos(float3(0,0.5,1)*6.283 + t*6.283);
                return saturate(0.5+0.5*c);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed inFlow(float2 uv)
            {
                float rad = _Angle * 0.0174533;
                float k = tan(rad);
                float slant = uv.x - uv.y / k;

                float total = _Interval + _Duration;
                float now = fmod(_Time.y, total);
                fixed flow = 0;

                if(now < _Duration)
                {
                    float t = now/_Duration;
                    float x = lerp(-1.2, 1.2, t);
                    float fade = smoothstep(0,0.1,t) * smoothstep(1,0.9,t);
                    float d = abs(slant - x)/_Width;
                    flow = exp(-d*d*3);
                    flow *= fade;
                }
                return flow;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                float flow = inFlow(i.uv);
                float mask = step(0.01, col.a);

                float3 c = _RainbowMode>0.5 ? Rainbow(_Time.y*_RainbowSpeed) : _FlowColor;
                col.rgb += c * flow * _FlowIntensity * mask;

                return col;
            }
            ENDCG
        }
    }
}