using TigerForge;
using UnityEngine;

public class CostumeManager : MonoBehaviour
{
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;
    [SerializeField] private Controller _mainCharacterController;

    private void Start()
    {
        ListenEvent();
        SetModel();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CLOSING_SHOP, SetModel);
    }

    private void SetModel()
    {
        if (_mainCharacterController == null) return;

        Costume currentUsingCostume = IngameDataManager.GetCurrentUsingCostume();
        if (currentUsingCostume == null) return;

        _mainCharacterController.SetModelMaterial(currentUsingCostume.GetCostume());
    }
}