using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleVision : MonoBehaviour
{
    public float visionCircleRadius => GameplaySystem.Instance.GetSeekerCircleVisionRadius();
    private Transform _thisPlayer => GetComponentInParent<SeekerVision>().GetThisPlayerTransform();

    private void OnDrawGizmos()
    {
        // Gizmos.DrawSphere(_inGamePlayer.position, visionCircleRadius);
        Handles.DrawWireDisc(_thisPlayer.position, new Vector3(0, 1, 0), visionCircleRadius);
    }

    public bool IsObjInSeekerVision(Transform obj)
    {
        float distance = Vector3.Distance(obj.position, _thisPlayer.position);

        if (distance >= visionCircleRadius) return false;

        return true;
    }
}