using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instance => instance;

    [SerializeField] private PetManager _petManager;
    [SerializeField] private CostumeManager _costumeManager;
    [SerializeField] private QuestManager _questManager;


    private void Awake()
    {
        HandleInstanceObject();
        LoadComponents();
    }

    private void HandleInstanceObject()
    {
        instance = this;
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _petManager = GetComponentInChildren<PetManager>();
        _costumeManager = GetComponentInChildren<CostumeManager>();
        _questManager = GetComponentInChildren<QuestManager>();
    }

    public void FinishQuest(int questID)
    {
        _questManager.FinishQuest(questID);
    }

    public List<Quest> GetTodayQuestList()
    {
        return _questManager.GetTodayQuestList();
    }
}