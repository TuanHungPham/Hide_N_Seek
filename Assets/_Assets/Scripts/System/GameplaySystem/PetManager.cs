using TigerForge;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;
    [SerializeField] private PlayerController _mainCharacterController;

    private void Start()
    {
        ListenEvent();
        CreatePet();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CLOSING_SHOP, CreatePet);
    }

    private void CreatePet()
    {
        if (_mainCharacterController == null) return;

        GameObject currentUsingPet = IngameDataManager.GetCurrentUsingPet();
        if (currentUsingPet == null) return;

        GameObject pet = Instantiate(currentUsingPet);

        pet.transform.position = _mainCharacterController.GetPetHolderPosition();

        SetupPetState(pet);
    }

    private void SetupPetState(GameObject pet)
    {
        Pet_MovingSystem petMovingSystem = pet.GetComponent<Pet_MovingSystem>();

        petMovingSystem.SetHolder(_mainCharacterController.GetPetHolder());
    }
}