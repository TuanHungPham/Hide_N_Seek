using UnityEngine;

public class JoystickInput : IGameInput
{
    public Vector2 InputValue { get; set; }
    [SerializeField] public Joystick joystick => GameObject.FindObjectOfType<FloatingJoystick>();

    public void SetInputValue()
    {
        var inputValue = InputValue;
        inputValue.x = joystick.Horizontal;
        inputValue.y = joystick.Vertical;
        InputValue = inputValue;
    }
}