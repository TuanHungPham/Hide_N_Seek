Shader "Lightweight Render Pipeline/Simple Lit Vision" {
	Properties {
		_BaseMap ("Base Map", 2D) = "white" {}
		_BaseColor ("Base Color", Vector) = (1,1,1,1)
		[HDR] _SpecColor ("Spec Color", Vector) = (0,0,0,0)
		_SpecPower ("Specular Power", Float) = 0
		[HDR] _EmissionColor ("Emission Color", Vector) = (0,0,0,0)
		[HideInInspector] _Cutoff ("_Cutoff", Float) = 0.5
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