Shader "Lightweight Render Pipeline/Customizable" {
	Properties {
		_BaseColor ("Color", Vector) = (0.5,0.5,0.5,1)
		[NoScaleOffset] _BaseMap ("Base", 2D) = "white" {}
		_SpecularPower ("Specular Power", Float) = 10
		[HDR] _SpecularColor ("Specular Color", Vector) = (0.5,0.5,0.5,1)
		[HideInInspector] _InvisibleFactor ("Invisible Factor", Float) = 0
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