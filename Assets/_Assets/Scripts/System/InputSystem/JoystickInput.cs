using UnityEngine;

public class JoystickInput : IGameInput
{
    #region public

    public Vector2 InputValue { get; set; }
    public Vector2 Direction { get; set; }
    private Joystick joystick => GameObject.FindObjectOfType<FloatingJoystick>();

    #endregion


    public void SetInputValue()
    {
        var inputValue = InputValue;
        inputValue.x = joystick.Horizontal;
        inputValue.y = joystick.Vertical;

        InputValue = inputValue;
    }

    public void SetInputDirection()
    {
    }
}