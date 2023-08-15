using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontVision : MonoBehaviour
{
    public float frontVisionRadius => GameplaySystem.Instance.GetSeekerFrontVisionRadius();

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(2, 2, frontVisionRadius));
    }
}