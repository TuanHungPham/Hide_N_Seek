using UnityEngine;

public class DebugPoint : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
}