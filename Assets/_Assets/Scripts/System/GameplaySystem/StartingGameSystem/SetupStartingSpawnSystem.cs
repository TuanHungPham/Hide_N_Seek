using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class SetupStartingSpawnSystem : MonoBehaviour
{
    [SerializeField] private Transform _allPlayerParent;
    [SerializeField] private Transform _mainCharacter;
    [SerializeField] private GameObject _otherPlayerPrefab;

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
    }

    private void Start()
    {
        SpawnMainCharacter();
        SpawnAllOtherPlayer();
    }

    private void SpawnMainCharacter()
    {
        _mainCharacter.transform.position = MapLevelSystem.Instance.GetMainPointPos();
    }

    private void SpawnAllOtherPlayer()
    {
        List<Vector3> startingPointList = MapLevelSystem.Instance.GetStartingPointList();

        for (int i = 0; i < startingPointList.Count; i++)
        {
            GameObject player = Instantiate(_otherPlayerPrefab);
            player.transform.position = startingPointList[i];
            player.transform.eulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);
            player.transform.SetParent(_allPlayerParent, true);

            player.gameObject.name = string.Format($"OtherPlayer[{i}]");
            Debug.Log($"{player.gameObject.name} --- Pos: {player.transform.position}");
        }

        Debug.Log("Spawning all player...");
        EmitSpawningPlayerEvent();
    }

    private void EmitSpawningPlayerEvent()
    {
        EventManager.EmitEvent(EventID.SPAWNING_PLAYER);
    }

    public Transform GetMainCharacterReference()
    {
        return _mainCharacter;
    }
}