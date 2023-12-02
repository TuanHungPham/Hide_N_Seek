using UnityEngine;
using UnityEngine.Serialization;

public class MovingSystem : MonoBehaviour
{
    #region private

    private InputSystem InputSystem => InputSystem.Instance;

    private const int RUNNING_ANIMATION_ID = 7;
    private const int WALKING_ANIMATION_ID = 8;

    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [Space(20)] [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform player;
    [SerializeField] private Controller _controller;

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
        _controller = GetComponentInParent<Controller>();
        player = transform.parent;

        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        Move();
        HandleGravity();
    }

    private void HandleGravity()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void Move()
    {
        float velX;
        float velZ;
        rb.velocity = Vector3.zero;
        MakeFootstep(false);

        if (!CanMove())
        {
            SetMovingAnimation(0);
            return;
        }

        SetPlayerMovingDirection();
        Vector2 inputValue = new Vector2();

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            velX = InputSystem.GetKeyboardInputValue().x;
            velZ = InputSystem.GetKeyboardInputValue().y;
        }
        else
        {
            inputValue = InputSystem.GetGameInputValue().normalized;
            velX = inputValue.x;
            velZ = inputValue.y;
        }

        if (InputSystem.GetGameInputValue() == Vector2.zero)
        {
            _controller.SetIdleAnimationState();
            MakeFootstep(false);
            return;
        }

        SetMovingAnimation(inputValue.magnitude);
        rb.velocity = new Vector3(velX * moveSpeed, rb.velocity.y, velZ * moveSpeed);
        MakeFootstep(true);
    }

    private void SetPlayerMovingDirection()
    {
        Vector3 movingDirection = new Vector3(InputSystem.GetGameInputValue().x, 0, InputSystem.GetGameInputValue().y);
        if (movingDirection.Equals(Vector3.zero)) return;

        Quaternion rotatation = Quaternion.LookRotation(movingDirection.normalized, Vector3.up);

        player.rotation = Quaternion.RotateTowards(player.rotation, rotatation, rotationSpeed);
    }

    private void MakeFootstep(bool set)
    {
        _controller.GetInGameState().SetIsMakingFootstep(set);
    }

    private void SetMovingAnimation(float value)
    {
        _controller.SetMovingAnimation(value);
    }

    private bool CanMove()
    {
        if (GameplaySystem.Instance.IsInHidingTimer() && _controller.GetInGameState().IsSeeker()) return false;
        else if (_controller.GetInGameState().IsCaught()) return false;
        return true;
    }
}