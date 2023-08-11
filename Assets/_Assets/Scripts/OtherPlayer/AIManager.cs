using System;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private AIMovingManager _aiMovingManager;

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
        _aiMovingManager = GetComponentInChildren<AIMovingManager>();
    }

    public AIMovingManager GetAIMovingManager()
    {
        return _aiMovingManager;
    }
}