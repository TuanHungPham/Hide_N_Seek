using UnityEngine;

public class InputSystem : TemporaryMonoSingleton<InputSystem>
{
    #region private

    [SerializeField] private Vector2 keyboardInputValue;

    [Space(20)] [SerializeField] private IGameInput gameInput;

    #endregion

    protected override void Awake()
    {
        base.Awake();

        gameInput = new JoystickInput();
    }

    private void Update()
    {
        SetGameInput();
    }

    private void SetGameInput()
    {
        gameInput.SetInputValue();
        gameInput.SetInputDirection();

        SetKeyboardInput();
    }

    private void SetKeyboardInput()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) return;
        keyboardInputValue.x = Input.GetAxis("Horizontal");
        keyboardInputValue.y = Input.GetAxis("Vertical");
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