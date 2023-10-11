using System;
using UnityEngine;
using System.Collections;

public class SSAutoAddUICamera : MonoBehaviour 
{
	[Obsolete("Obsolete")]
	private void Awake()
	{
		UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(gameObject, "Assets/SSSystem/Scripts/Compatibility/nGUI/SSAutoAddUICamera.cs (8,3)", "UICamera");
	}
}
