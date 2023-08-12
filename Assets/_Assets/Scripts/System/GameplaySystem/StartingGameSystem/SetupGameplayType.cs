using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class SetupGameplayType : MonoBehaviour
{
    #region private

    [SerializeField] private bool isSeekerGameplay;
    [SerializeField] private int numberOfSeeker;

    #endregion

    private void Awake()
    {
        LoadComponents();
        ListenEvent();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.INITIALIZING_ALL_PLAYER_LIST, HandleSettingUpGameplay);
    }

    private void HandleSettingUpGameplay()
    {
        if (!isSeekerGameplay)
        {
            SetupPlayAsHider();
            return;
        }

        SetupPlayAsSeeker();
    }

    private void SetupPlayAsSeeker()
    {
    }

    private void SetupPlayAsHider()
    {
        // Debug.Log("Setting up playing as hider gameplay...");
        List<Transform> allPlayerList = GameplaySystem.Instance.GetAllPlayerList();

        int count = 0;

        foreach (Transform player in allPlayerList)
        {
            if (count >= numberOfSeeker) break;

            Controller playerController = player.GetComponent<Controller>();

            if (playerController.GetPlayerType() == PlayerType.MAIN_CHARACTER ||
                playerController.GetInGameState().IsSeeker())
                continue;

            Debug.Log("Setting up playing as hider gameplay...");
            Debug.Log("COUNT: " + count);
            playerController.SetSeekerState(true);
            count++;
        }

        EmitSettedUpGameplayEvent();
    }

    private void EmitSettedUpGameplayEvent()
    {
        EventManager.EmitEvent(EventID.SETTED_UP_GAMEPLAY);
    }
}