using UnityEditor;
using UnityEngine;

public class CircleVision : MonoBehaviour
{
    public float visionCircleRadius => GameplaySystem.Instance.GetSeekerCircleVisionRadius();
    private Transform _thisPlayer => GetComponentInParent<SeekerVisionInteractingSystem>().GetThisPlayerTransform();
    [SerializeField] private LayerMask obstacleLayerMask;

    public bool IsObjInSeekerVision(Transform obj)
    {
        var thisPlayerPosition = _thisPlayer.position;
        var objPosition = obj.position;

        float distance = Vector3.Distance(objPosition, thisPlayerPosition);
        Vector3 direction = (objPosition - thisPlayerPosition).normalized;

        if (distance >= visionCircleRadius) return false;

        bool hit = Physics.Raycast(_thisPlayer.position, direction, distance, obstacleLayerMask);
        if (hit) return false;

        return true;
    }
}