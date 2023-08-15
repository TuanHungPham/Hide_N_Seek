using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class SetupGameplayType : MonoBehaviour
{
    #region private

    [SerializeField] private bool isSeekerGameplay;
    [SerializeField] private int numberOfSeeker;

    [Space(20)] [SerializeField] private StartingGameSystem _startingGameSystem;

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
        _startingGameSystem = GetComponentInParent<StartingGameSystem>();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.INITIALIZING_ALL_PLAYER_LIST, HandleSettingUpGameplay);
    }

    private void HandleSettingUpGameplay()
    {
        List<Transform> allPlayerList = GameplaySystem.Instance.GetAllPlayerList();

        if (!isSeekerGameplay)
        {
            SetupPlayAsHider(allPlayerList);

            EmitSettedUpGameplayEvent();
            return;
        }

        SetupPlayAsSeeker(allPlayerList);

        EmitSettedUpGameplayEvent();
    }

    private void SetupPlayAsSeeker(List<Transform> playerList)
    {
        int seekerCount = 0;
        SetPlayerAsSeeker();
        seekerCount++;

        SetOtherPlayerAsSeeker(playerList, seekerCount);
    }

    private void SetupPlayAsHider(List<Transform> playerList)
    {
        // Debug.Log("Setting up playing as hider gameplay...");

        int seekerCount = 0;

        SetOtherPlayerAsSeeker(playerList, seekerCount);
    }

    private void SetPlayerAsSeeker()
    {
        Transform mainCharacter = _startingGameSystem.GetMainCharacterReference();
        Controller playerController = mainCharacter.GetComponent<Controller>();

        playerController.SetSeekerState(true);
    }

    private void SetOtherPlayerAsSeeker(List<Transform> playerList, int seekerCount)
    {
        foreach (Transform player in playerList)
        {
            if (seekerCount >= numberOfSeeker) break;

            Controller playerController = player.GetComponent<Controller>();

            if (playerController.GetPlayerType() == PlayerType.MAIN_CHARACTER ||
                playerController.GetInGameState().IsSeeker())
                continue;

            Debug.Log("Setting up playing as hider gameplay...");
            Debug.Log("COUNT: " + seekerCount);
            playerController.SetSeekerState(true);
            seekerCount++;
        }
    }

    private void EmitSettedUpGameplayEvent()
    {
        EventManager.EmitEvent(EventID.SETTED_UP_GAMEPLAY);
    }

    public bool IsSeekerGameplay()
    {
        return isSeekerGameplay;
    }
}