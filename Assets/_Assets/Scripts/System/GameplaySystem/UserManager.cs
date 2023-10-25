using UnityEngine;

public class UserManager : MonoBehaviour
{
    [SerializeField] private string _userName;

    public void SetUsername(string name)
    {
        _userName = name;
    }

    public string GetUsername()
    {
        return _userName;
    }
}