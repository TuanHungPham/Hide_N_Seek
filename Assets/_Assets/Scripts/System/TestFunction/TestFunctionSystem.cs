using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class TestFunctionSystem : MonoBehaviour
{
    public void OnResetQuest()
    {
        IngameDataManager.Instance.GetQuestDataManager().RandomTodayQuestTemplate();
        EventManager.EmitEvent(EventID.QUEST_RESETTING);
    }

    public void OnEndLevel()
    {
        GameplaySystem.Instance.SetGameplayTimer(0);
    }
}