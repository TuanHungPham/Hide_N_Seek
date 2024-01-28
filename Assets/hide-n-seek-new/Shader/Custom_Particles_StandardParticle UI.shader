Shader "Custom/Particles/StandardParticle UI" {
	Properties {
		_MainTex ("Particle Texture", 2D) = "white" {}
		_TintColor ("Tint Color", Vector) = (1,1,1,1)
		_Intensity ("Intensity", Range(0, 100)) = 1
		_GradientMap ("Gradient Map", 2D) = "red" {}
		_DissolveSmoothness ("Smoothness", Range(0.01, 0.5)) = 0.1
		_InflateStrength ("Inflate Strength", Float) = 0
		_AlphaPower ("Alpha Power", Float) = 1
		[HideInInspector] _Color ("Tint", Vector) = (1,1,1,1)
		[HideInInspector] _Mode ("__mode", Float) = 0
		[HideInInspector] _SrcBlend ("__src", Float) = 1
		[HideInInspector] _DstBlend ("__dst", Float) = 0
		[HideInInspector] _ZWrite ("__zw", Float) = 1
		[HideInInspector] _Cull ("__cull", Float) = 2
		[HideInInspector] _ZTest ("__zt", Float) = 7
		[HideInInspector] _RenderQueueOffset ("_RenderQueueOffset", Float) = 0
		[HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
		[HideInInspector] _Stencil ("Stencil ID", Float) = 0
		[HideInInspector] _StencilOp ("Stencil Operation", Float) = 0
		[HideInInspector] _StencilWriteMask ("Stencil Write Mask", Float) = 255
		[HideInInspector] _StencilReadMask ("Stencil Read Mask", Float) = 255
		[HideInInspector] _ColorMask ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] [HideInInspector] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "StandardParticle"
}