using UnityEngine;

public class BeingFoundHandler : MonoBehaviour
{
    [SerializeField] private GameObject _cage;
    [SerializeField] private GameObject _helpMeConversation;
    [SerializeField] private GameObject _cloudVFX;

    public void SetupBeingFoundState(bool set)
    {
        _cage.gameObject.SetActive(set);
        _cloudVFX.gameObject.SetActive(set);
        // _helpMeConversation.gameObject.SetActive(set);
    }
}