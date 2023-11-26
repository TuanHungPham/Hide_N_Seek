using System;
using UnityEditor;
using UnityEngine;

public class CircleVision : MonoBehaviour
{
    public float visionCircleRadius => GameplaySystem.Instance.GetSeekerCircleVisionRadius();
    private Transform _thisPlayer => GetComponentInParent<SeekerVisionInteractingSystem>().GetThisPlayerTransform();
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private Light _spotLight;

    private void Awake()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _spotLight = GetComponent<Light>();
    }

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

    public void SetLightHeight(float height)
    {
        var transformLocalPosition = transform.localPosition;
        transformLocalPosition.y = height;
        transform.localPosition = transformLocalPosition;
    }

    public void SetLightIntensity(float intensity)
    {
        _spotLight.intensity = intensity;
    }

    public void SetLightRange(float range)
    {
        _spotLight.range = range;
    }
}