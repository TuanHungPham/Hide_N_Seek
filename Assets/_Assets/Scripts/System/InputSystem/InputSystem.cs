using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private static InputSystem instance;

    public static InputSystem Instance
    {
        get => instance;
    }

    #region private

    [SerializeField] private Vector2 keyboardInputValue;

    [Space(20)] [SerializeField] private IGameInput gameInput;

    #endregion

    private void Awake()
    {
        instance = this;
        gameInput = new JoystickInput();
    }

    private void Update()
    {
        SetInputValue();
    }

    private void SetInputValue()
    {
        gameInput.SetInputValue();

        SetKeyboardInput();
    }

    private void SetKeyboardInput()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) return;
        keyboardInputValue.x = Input.GetAxis("Horizontal");
        keyboardInputValue.y = Input.GetAxis("Vertical");
    }

    private void SetupMovingInput()
    {
    }

    public Vector2 GetGameInputValue()
    {
        return gameInput.InputValue;
    }

    public Vector2 GetKeyboardInputValue()
    {
        return keyboardInputValue;
    }
}