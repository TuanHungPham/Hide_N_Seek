using System;
using TigerForge;
using UnityEngine;

public class CostumeManager : MonoBehaviour
{
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;
    [SerializeField] private Controller _mainCharacterController;

    private void Awake()
    {
        EventManager.StartListening(EventID.CLOSING_SHOP, SetModel);
    }

    private void Start()
    {
        SetModel();
    }

    private void SetModel()
    {
        if (_mainCharacterController == null) return;

        Debug.Log("Is In Here...");
        Costume currentUsingCostume = IngameDataManager.GetCurrentUsingCostume();
        if (currentUsingCostume == null) return;

        _mainCharacterController.SetModelMaterial(currentUsingCostume.GetCostume());
    }
}