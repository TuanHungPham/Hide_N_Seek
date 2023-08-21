using UnityEngine;

public class MovingSystem : MonoBehaviour
{
    #region private

    private InputSystem InputSystem => InputSystem.Instance;

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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float velX;
        float velZ;
        rb.velocity = Vector3.zero;
        MakeFootstep(false);

        if (!CanMove()) return;

        SetPlayerMovingDirection();

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            velX = InputSystem.GetKeyboardInputValue().x;
            velZ = InputSystem.GetKeyboardInputValue().y;
        }
        else
        {
            Vector2 inputValue = InputSystem.GetGameInputValue().normalized;
            velX = inputValue.x;
            velZ = inputValue.y;
        }

        if (InputSystem.GetGameInputValue() == Vector2.zero)
        {
            MakeFootstep(false);
            return;
        }

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

    private bool CanMove()
    {
        if (GameplaySystem.Instance.IsInHidingTimer() && _controller.GetInGameState().IsSeeker()) return false;
        else if (_controller.GetInGameState().IsCaught()) return false;
        return true;
    }
}