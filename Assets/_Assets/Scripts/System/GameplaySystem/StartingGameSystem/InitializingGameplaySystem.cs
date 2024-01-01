using System.Collections;
using UnityEngine;
using TigerForge;
using UnityEngine.Serialization;

public class InitializingGameplaySystem : MonoBehaviour
{
    #region private

    [SerializeField] private bool _isGameStarting;

    [Space(20)] [Header("System")] [SerializeField]
    private SetupGameplayTypeSystem setupGameplayTypeSystem;

    [SerializeField] private SetupStartingSpawnSystem setupStartingSpawnSystem;

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
        setupGameplayTypeSystem = GetComponentInChildren<SetupGameplayTypeSystem>();
        setupStartingSpawnSystem = GetComponentInChildren<SetupStartingSpawnSystem>();

        _isGameStarting = false;
    }

    private void Start()
    {
        StartCoroutine(CheckGameStartingState());
    }

    IEnumerator CheckGameStartingState()
    {
        while (!_isGameStarting)
        {
            if (InputSystem.Instance.GetGameInputValue() != Vector2.zero)
            {
                _isGameStarting = true;
                EmitGameStartedEvent();
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void EmitGameStartedEvent()
    {
        EventManager.EmitEvent(EventID.GAME_STARTED);
    }

    public Transform GetMainCharacterReference()
    {
        return setupStartingSpawnSystem.GetMainCharacterReference();
    }

    public void SetGameplayType(bool isSeekerGameplay)
    {
        setupGameplayTypeSystem.SetGameplayType(isSeekerGameplay);
    }

    public void SetNumberOfSeeker(int number)
    {
        setupGameplayTypeSystem.SetNumberOfSeeker(number);
    }

    public bool IsSeekerGameplay()
    {
        return setupGameplayTypeSystem.IsSeekerGameplay();
    }

    public bool IsGameStarting()
    {
        return _isGameStarting;
    }
}