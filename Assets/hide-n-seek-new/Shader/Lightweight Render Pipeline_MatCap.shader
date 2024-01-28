Shader "Lightweight Render Pipeline/MatCap" {
	Properties {
		[Toggle(_UNITY_SHADOWS)] _UnityShadows ("Unity Shadows", Float) = 0
		[Toggle(_SUPPORT_VISION)] _SupportVision ("Seeker Vision", Float) = 0
		[Toggle(_MATCAP_REFL)] _EnableMatCap ("Enable Mat Cap", Float) = 0
		_MatCapRefIntensity ("MatCap Refl Intensity", Float) = 1
		[NoScaleOffset] _BaseMap ("MatCap Reflection", 2D) = "white" {}
		_Diffuse ("Diffuse Texture", 2D) = "white" {}
		[HDR] _Color ("Tint", Vector) = (0.5,0.5,0.5,1)
		[HDR] _XRaysOverrideColor ("XRays Override Color", Vector) = (0.5,0.5,0.5,0)
		[HideInInspector] _SpecularPower ("Specular Power", Float) = 10
		[HideInInspector] _InvisibleFactor ("Invisible Factor", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}