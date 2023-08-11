using UnityEngine;

public interface IGameInput
{
    Vector2 InputValue { get; set; }
    Vector2 Direction { get; set; }

    void SetInputValue();
    void SetInputDirection();
}