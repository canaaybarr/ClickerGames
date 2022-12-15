﻿// Toony Colors Pro+Mobile 2
// (c) 2014-2019 Jean Moreno
Shader "Toony Colors Pro 2/MobileStencil"
{
   Properties
   {
      //TOONY COLORS
      _Color ("Color", Color) = (1,1,1,1)
      _HColor ("Highlight Color", Color) = (0.785,0.785,0.785,1.0)
      _SColor ("Shadow Color", Color) = (0.195,0.195,0.195,1.0)
      //DIFFUSE
      _MainTex ("Main Texture (RGB) Spec/MatCap Mask (A) ", 2D) = "white" {}
      //TOONY COLORS RAMP
      [TCP2Gradient] _Ramp ("#RAMPT# Toon Ramp (RGB)", 2D) = "gray" {}
      _RampThreshold ("#RAMPF# Ramp Threshold", Range(0,1)) = 0.5
      _RampSmooth ("#RAMPF# Ramp Smoothing", Range(0.01,1)) = 0.1
      //BUMP
      _BumpMap ("#NORM# Normal map (RGB)", 2D) = "bump" {}
      [IntRange] _StencilRef ("Stencil Reference Value", Range(0,255)) = 0
   }
   SubShader
   {
      Tags { "RenderType"="Opaque" }
      LOD 200
      Stencil{
         Ref [_StencilRef]
         Comp Equal
      }
      CGPROGRAM
      #include "Include/TCP2_Include.cginc"
      #pragma surface surf ToonyColors noforwardadd interpolateview halfasview
      #pragma target 2.0
      #pragma shader_feature TCP2_DISABLE_WRAPPED_LIGHT
      #pragma shader_feature TCP2_RAMPTEXT
      #pragma shader_feature TCP2_BUMP
      //stencil operation
      //================================================================
      // VARIABLES
      fixed4 _Color;
      sampler2D _MainTex;
   #if TCP2_BUMP
      sampler2D _BumpMap;
   #endif
      struct Input
      {
         half2 uv_MainTex : TEXCOORD0;
   #if TCP2_BUMP
         half2 uv_BumpMap : TEXCOORD1;
   #endif
      };
      //================================================================
      // SURFACE FUNCTION
      void surf (Input IN, inout SurfaceOutput o)
      {
         half4 main = tex2D(_MainTex, IN.uv_MainTex);
         o.Albedo = main.rgb * _Color.rgb;
         o.Alpha = main.a * _Color.a;
   #if TCP2_BUMP
         //Normal map
         o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
   #endif
      }
      ENDCG
   }
   Fallback "Diffuse"
   CustomEditor "TCP2_MaterialInspector"
}