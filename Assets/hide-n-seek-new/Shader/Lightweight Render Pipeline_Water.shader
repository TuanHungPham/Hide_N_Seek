Shader "Lightweight Render Pipeline/Water" {
	Properties {
		_BaseColor ("Base Color", Vector) = (0.5,0.5,0.5,1)
		_BaseMap ("Base Map (RGB) Smoothness / Alpha (A)", 2D) = "white" {}
		[HDR] _ReflectionColor ("Reflection Color", Vector) = (0.5,0.5,0.5,1)
		_FresnelPower ("Fresnel Power", Float) = 1
		[Toggle(_SUPPORT_VISION)] _SupportVision ("Support Vision", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}