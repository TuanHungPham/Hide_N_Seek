using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class CatchingPointPanel : MonoBehaviour
{
    [SerializeField] private List<CatchingPointUI> catchingPointUIList = new List<CatchingPointUI>();
    [SerializeField] private CatchingPointUI _catchingPointUIPrefab;
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    private void Awake()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.END_HIDING_TIME, InitializeCatchingPointList);
        EventManager.StartListening(EventID.CAUGHT_HIDER, SetPointWhenCatchingHider);
        EventManager.StartListening(EventID.RESCUING_HIDER, SetPointWhenRescuingHider);
    }

    private void InitializeCatchingPointList()
    {
        int numberOfHider = GameplaySystem.GetNumberOfHider();
        int requirementNumberOfCaughtHider = GameplaySystem.GetRequirementNumberOfCaughtHider();
        Debug.Log("Checking... " + numberOfHider + " " + requirementNumberOfCaughtHider);

        for (int i = 1; i <= numberOfHider; i++)
        {
            CatchingPointUI catchingPointUI = Instantiate(_catchingPointUIPrefab, transform, true);
            catchingPointUI.transform.localScale = Vector3.one;

            SetupCatchingPointUI(i, requirementNumberOfCaughtHider, catchingPointUI);

            catchingPointUIList.Add(catchingPointUI);
        }
    }

    private void SetupCatchingPointUI(int index, int requirementNumberOfCaughtHider, CatchingPointUI catchingPointUI)
    {
        catchingPointUI.SetBright(false);

        if (index > requirementNumberOfCaughtHider)
        {
            catchingPointUI.SetIsPlusPoint(true);
        }
        else
        {
            catchingPointUI.SetIsPlusPoint(false);
        }
    }

    private void SetPointWhenCatchingHider()
    {
        for (int i = 0; i < catchingPointUIList.Count; i++)
        {
            if (catchingPointUIList[i].IsBright()) continue;

            catchingPointUIList[i].SetBright(true);
            return;
        }
    }

    private void SetPointWhenRescuingHider()
    {
        for (int i = catchingPointUIList.Count - 1; i >= 0; i--)
        {
            if (!catchingPointUIList[i].IsBright()) continue;

            catchingPointUIList[i].SetBright(false);
            return;
        }
    }
}