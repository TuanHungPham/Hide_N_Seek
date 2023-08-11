using System;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private float cameraHeight;
    [SerializeField] private float cameraDepth;

    [Space(20)] [SerializeField] private Transform mainCharacter;

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    private void LateUpdate()
    {
        FollowMainCharacter();
    }

    private void FollowMainCharacter()
    {
        transform.position = new Vector3(mainCharacter.position.x, mainCharacter.position.y + cameraHeight,
            mainCharacter.position.z - cameraDepth);
    }
}