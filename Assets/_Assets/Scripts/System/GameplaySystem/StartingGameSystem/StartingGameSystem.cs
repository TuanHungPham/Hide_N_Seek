using System.Collections;
using UnityEngine;

public class StartingGameSystem : MonoBehaviour
{
    #region private

    [SerializeField] private bool _isGameStarting;

    [Space(20)] [Header("System")] [SerializeField]
    private SetupGameplayType _setupGameplayType;

    [SerializeField] private SetupStartingSpawn _setupStartingSpawn;

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
        _setupGameplayType = GetComponentInChildren<SetupGameplayType>();
        _setupStartingSpawn = GetComponentInChildren<SetupStartingSpawn>();

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
            Debug.Log("Checking...");
            if (InputSystem.Instance.GetGameInputValue() != Vector2.zero)
            {
                _isGameStarting = true;
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public Transform GetMainCharacterReference()
    {
        return _setupStartingSpawn.GetMainCharacterReference();
    }

    public bool IsSeekerGameplay()
    {
        return _setupGameplayType.IsSeekerGameplay();
    }

    public bool IsGameStarting()
    {
        return _isGameStarting;
    }
}