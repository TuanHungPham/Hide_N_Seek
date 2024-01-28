Shader "Custom/Particles/StandardParticle" {
	Properties {
		_MainTex ("Particle Texture", 2D) = "white" {}
		_TintColor ("Tint Color", Vector) = (1,1,1,1)
		_ScreenRatio ("Image Ratio", Range(0, 5)) = 1.77
		_Intensity ("Intensity", Range(0, 100)) = 1
		_FresnelIntensity ("Fresnel Intensity", Range(0, 100)) = 0
		_GradientMap ("Gradient Map", 2D) = "red" {}
		_DissolveSmoothness ("Smoothness", Range(0.001, 0.5)) = 0.1
		_SoftParticleThreshold ("Softness", Range(0.1, 150)) = 0
		_FresnelLUT ("Fresnel LUT", 2D) = "white" {}
		_CameraFadeDistance ("Camera Fade Distance", Range(0, 1000)) = 1
		_CameraFadePower ("Camera Fade Power", Range(0, 1000)) = 1
		[HideInInspector] _Mode ("__mode", Float) = 0
		[HideInInspector] _SrcBlend ("__src", Float) = 1
		[HideInInspector] _DstBlend ("__dst", Float) = 0
		[HideInInspector] _ZWrite ("__zw", Float) = 1
		[HideInInspector] _Cull ("__cull", Float) = 2
		[HideInInspector] _ZTest ("__zt", Float) = 7
		[HideInInspector] _RenderQueueOffset ("_RenderQueueOffset", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	//CustomEditor "StandardParticle"
}