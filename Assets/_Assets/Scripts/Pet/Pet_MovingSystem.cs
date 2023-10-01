using System;
using UnityEngine;
using UnityEngine.AI;

public class Pet_MovingSystem : MonoBehaviour
{
    [SerializeField] private Transform _holder;

    [Space(10)] [Header("Auto getting reference by reset")] [SerializeField]
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private Pet_AnimationSystem _petAnimationSystem;

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
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _petAnimationSystem = GetComponentInChildren<Pet_AnimationSystem>();
    }

    private void FixedUpdate()
    {
        MoveAlongCharacter();
    }

    private void MoveAlongCharacter()
    {
        if (!CanMove())
        {
            _petAnimationSystem.SetIdleAnimation();
            return;
        }

        _petAnimationSystem.SetMovingAnimation();
        _navMeshAgent.SetDestination(_holder.position);
    }

    private bool CanMove()
    {
        float distanceToCharacter = Vector3.Distance(transform.position, _holder.position);
        if (distanceToCharacter <= 1f) return false;
        return true;
    }

    public void SetHolder(Transform holder)
    {
        _holder = holder;
    }
}