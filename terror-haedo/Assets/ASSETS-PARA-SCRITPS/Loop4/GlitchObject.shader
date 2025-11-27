Shader "Custom/URP_ObjectGlitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Intensity ("Glitch Intensity", Range(0,1)) = 0.5
        _RGBSplit ("RGB Split Amount", Range(0,0.05)) = 0.01
        _LineSpeed ("Line Speed", Range(0,50)) = 20
        _Distortion ("Distortion Amount", Range(0,0.1)) = 0.03
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float _Intensity;
            float _RGBSplit;
            float _LineSpeed;
            float _Distortion;

            // --- Random helper ---
            float rand(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                return OUT;
            }

            float4 frag (Varyings IN) : SV_Target
            {
                float t = _Time.y;

                // Scanline glitch index (renamed from "line")
                float scan = floor(IN.uv.y * 40.0 + sin(t * _LineSpeed));

                float noise = rand(float2(scan, t));

                // Distorted UV
                float2 distortedUV = IN.uv;
                distortedUV.x += (noise - 0.5) * _Distortion * _Intensity;

                // RGB split
                float2 uvR = distortedUV + float2(_RGBSplit, 0);
                float2 uvB = distortedUV - float2(_RGBSplit, 0);

                float3 col;
                col.r = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvR).r;
                col.g = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, distortedUV).g;
                col.b = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvB).b;

                // Flicker
                float flicker = step(0.95, rand(float2(t, scan)));
                col *= lerp(1, 0, flicker * _Intensity);

                return float4(col, 1);
            }

            ENDHLSL
        }
    }
}
