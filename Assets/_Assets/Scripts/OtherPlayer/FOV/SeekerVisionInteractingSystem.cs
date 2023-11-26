using System;
using TigerForge;
using UnityEditor;
using UnityEngine;

public enum eVisionType
{
    FRONT_VISION,
    CIRCLE_VISION
}

public class SeekerVisionInteractingSystem : MonoBehaviour
{
    #region private

    [SerializeField] private Transform _thisPlayer;
    [SerializeField] private ePlayerType _playerType;
    [SerializeField] private CircleVision _circleVision;
    [SerializeField] private FrontVision _frontVision;

    private GameplayManager GameplayManager => GameplayManager.Instance;

    private float _visionCircleRadius => GameplaySystem.Instance.GetSeekerCircleVisionRadius();

    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CATCHING_PLAYER,
            () =>
            {
                Controller caughtPlayerController = _frontVision.GetCaughtPlayerController();
                CatchTarget(caughtPlayerController);
            });
    }

    private void LoadComponents()
    {
        _circleVision = GetComponentInChildren<CircleVision>();
        _frontVision = GetComponentInChildren<FrontVision>();

        Debug.Log($"LAYER NAME: {_thisPlayer.gameObject.layer}");
    }

    private void Update()
    {
        CheckCollidingAsSeekerVision();
    }

    private void CheckCollidingAsSeekerVision()
    {
        if (!CanCheckColliding()) return;

        foreach (var player in GameplaySystem.Instance.GetHiderList())
        {
            Controller playerController = player.GetComponent<Controller>();
            if (playerController.GetInGameState().IsCaught()) continue;

            if (_circleVision.IsObjInSeekerVision(player))
            {
                CatchTarget(playerController);
            }
        }
    }

    private void CatchTarget(Controller targetController)
    {
        if (targetController == null || !CanCheckColliding()) return;

        targetController.SetCaughtState(true);

        EmitCatchingHiderEvent();

        UpdateInGameAchievement();
    }

    private void EmitCatchingHiderEvent()
    {
        EventManager.EmitEvent(EventID.CAUGHT_HIDER);
    }

    private bool CanCheckColliding()
    {
        if (GameplaySystem.Instance.IsInHidingTimer()) return false;

        return true;
    }

    public Transform GetThisPlayerTransform()
    {
        return _thisPlayer;
    }

    public void SetLightRange(eVisionType visionType, float value)
    {
        switch (visionType)
        {
            case eVisionType.FRONT_VISION:
                _frontVision.SetLightRange(value);
                break;
            case eVisionType.CIRCLE_VISION:
                _circleVision.SetLightRange(value);
                break;
        }
    }

    public void SetLightHeight(eVisionType visionType, float value)
    {
        switch (visionType)
        {
            case eVisionType.FRONT_VISION:
                _frontVision.SetLightHeight(value);
                break;
            case eVisionType.CIRCLE_VISION:
                _circleVision.SetLightHeight(value);
                break;
        }
    }

    public void SetLightIntensity(eVisionType visionType, float value)
    {
        switch (visionType)
        {
            case eVisionType.FRONT_VISION:
                _frontVision.SetLightIntensity(value);
                break;
            case eVisionType.CIRCLE_VISION:
                _circleVision.SetLightIntensity(value);
                break;
        }
    }

    private void UpdateInGameAchievement()
    {
        if (_playerType == ePlayerType.NPC) return;

        InGameManager.Instance.AddAchievementPoint(eAchievementType.CATCHING_TIME);
        AddBonusCoin();
    }

    private void AddBonusCoin()
    {
        GameplayManager.AddCoin(eAddingCoinType.INTERACTING_BONUS_COIN, 10);
    }
}