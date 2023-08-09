using System;
using UnityEngine;

public class MovingSystem : MonoBehaviour
{
    #region private

    private InputSystem InputSystem => InputSystem.Instance;

    [SerializeField] private float moveSpeed;

    [Space(20)] [SerializeField] private Rigidbody rb;

    #endregion

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
        rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float velX = 0;
        float velZ = 0;

        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            velX = InputSystem.GetKeyboardInputValue().x;
            velZ = InputSystem.GetKeyboardInputValue().y;
        }
        else
        {
            velX = InputSystem.GetGameInputValue().x;
            velZ = InputSystem.GetGameInputValue().y;
        }

        rb.velocity = new Vector3(velX * moveSpeed, rb.velocity.y, velZ * moveSpeed);
    }
}