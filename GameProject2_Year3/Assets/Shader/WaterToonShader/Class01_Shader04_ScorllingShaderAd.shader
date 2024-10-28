// CGE 499 is for declare catagory
Shader "CGE499/Class01/Class01_Shader04_ScorllingShaderAd"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _SecondaryTex ("Secondary Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ScrollSpeedMain ("MainTex Scroll Speed", Vector) = (0.1, 0.0, 0, 0)
        _ScrollSpeedSecondary ("SecondaryTex Scroll Speed", Vector) = (0.0, 0.1, 0, 0)
        _BlendFactor ("Blend Factor", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Standard
        #include "UnityCG.cginc"
 
        sampler2D _MainTex;
        sampler2D _SecondaryTex;
        float4 _ScrollSpeedMain;
        float4 _ScrollSpeedSecondary;
        fixed4 _Color;
        float _BlendFactor;
 
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_SecondaryTex;
        };
 
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 scrolledUV_Main = IN.uv_MainTex + _ScrollSpeedMain.xy * _Time.y;
            float2 scrolledUV_Secondary = IN.uv_SecondaryTex + _ScrollSpeedSecondary.xy * _Time.y;
            fixed4 mainTexColor = tex2D(_MainTex, scrolledUV_Main);
            fixed4 secondaryTexColor = tex2D(_SecondaryTex, scrolledUV_Secondary);
            fixed4 blendedColor = lerp(mainTexColor, secondaryTexColor, _BlendFactor);
            o.Albedo = blendedColor.rgb * _Color.rgb;
        }
        ENDCG
    }

    // if code up there doesn't work
    Fallback "Diffuse"
}
