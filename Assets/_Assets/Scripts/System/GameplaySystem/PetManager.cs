using TigerForge;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;
    [SerializeField] private PlayerController _mainCharacterController;

    private void Start()
    {
        ListenEvent();
        SetupPetToCharacter();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CLOSING_SHOP, SetupPetToCharacter);
    }

    private void SetupPetToCharacter()
    {
        if (_mainCharacterController == null) return;

        GameObject currentUsingPet = IngameDataManager.GetCurrentUsingPet();
        if (currentUsingPet == null) return;

        _mainCharacterController.ChangePet(currentUsingPet);
    }
}