using System;
using System.Collections;
using TigerForge;
using UnityEngine;

public class HearingSystem : MonoBehaviour
{
    #region public

    public Color color;
    public Vector3 normal;
    public Vector3 from => transform.forward;
    public float hearingRange;

    #endregion

    #region private

    [SerializeField] private float checkingDelayTime;

    [Space(20)] [SerializeField] private Transform _currentAIPlayer;
    [SerializeField] private TriggeredSystem _triggeredSystem;
    [SerializeField] private Transform _footstepSoundMaking;

    #endregion

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
        _triggeredSystem = GetComponentInParent<TriggeredSystem>();
    }

    private void Start()
    {
        StartCoroutine(HandleHearingAround());
    }

    IEnumerator HandleHearingAround()
    {
        while (true)
        {
            if (GameplaySystem.Instance.IsTimeUp())
            {
                yield break;
            }

            HearAround();
            yield return new WaitForSeconds(checkingDelayTime);
        }
    }

    private void HearAround()
    {
        foreach (var hider in GameplaySystem.Instance.GetHiderList())
        {
            float distance = Vector3.Distance(_currentAIPlayer.position, hider.position);
            if (distance >= hearingRange) continue;

            Controller controller = hider.GetComponent<Controller>();
            if (!controller.GetInGameState().IsMakingFootstep()) continue;

            _footstepSoundMaking = hider;

            _triggeredSystem.SetTriggeredWhenHearingFootstep(true);
            return;
        }

        _triggeredSystem.SetTriggeredWhenHearingFootstep(false);
    }

    public Transform GetFootstepSoundMaking()
    {
        return _footstepSoundMaking;
    }
}