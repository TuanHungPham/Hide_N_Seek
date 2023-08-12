using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class SetupStartingSpawn : MonoBehaviour
{
    [SerializeField] private Transform allPlayerParent;
    [SerializeField] private Transform mainCharacter;
    [SerializeField] private GameObject otherPlayerPrefab;

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
        SpawnAllPlayer();
    }

    private void SpawnMainCharacter()
    {
        mainCharacter.transform.position = MapLevelSystem.Instance.GetMainPointPos();
    }

    private void SpawnAllPlayer()
    {
        List<Vector3> startingPointList = MapLevelSystem.Instance.GetStartingPointList();

        for (int i = 1; i < startingPointList.Count; i++)
        {
            GameObject player = Instantiate(otherPlayerPrefab);
            player.transform.position = startingPointList[i];
            player.transform.SetParent(allPlayerParent, true);
        }

        Debug.Log("Spawning all player...");
        EmitSpawningPlayerEvent();
    }

    private void EmitSpawningPlayerEvent()
    {
        EventManager.EmitEvent(EventID.SPAWNING_PLAYER);
    }
}