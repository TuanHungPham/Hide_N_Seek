using UnityEngine;

public interface IGameInput
{
    Vector2 InputValue { get; set; }

    void SetInputValue();
}