using UnityEngine;

public class PetHolderHandler : MonoBehaviour
{
    [Header("Auto Reference")] [SerializeField]
    private Transform _petHolder;

    [Space(20)] [Header("Manual Reference")] [SerializeField]
    private GameObject _currentHoldingPet;

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _petHolder = transform;
    }

    public void SetPetToHolder(GameObject pet)
    {
        Pet_MovingSystem petMovingSystem = pet.GetComponent<Pet_MovingSystem>();
        petMovingSystem.SetHolder(_petHolder);

        _currentHoldingPet = pet;
    }

    public void ChangePet(GameObject pet)
    {
        if (_currentHoldingPet != null)
        {
            Destroy(_currentHoldingPet);
        }

        GameObject newPet = Instantiate(pet);
        newPet.transform.position = _petHolder.position;

        SetPetToHolder(newPet);
    }

    public Transform GetPetHolder()
    {
        return _petHolder;
    }

    public Vector3 GetPetHolderPosition()
    {
        return _petHolder.position;
    }
}