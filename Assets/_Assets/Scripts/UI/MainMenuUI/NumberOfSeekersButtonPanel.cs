using System.Collections.Generic;
using UnityEngine;

public class NumberOfSeekersButtonPanel : MonoBehaviour
{
    [SerializeField] private List<SeekerNumberButton> buttonList = new List<SeekerNumberButton>();

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
        InitializeButtonList();
        InitializeButtonState();
    }

    private void InitializeButtonList()
    {
        foreach (Transform button in transform)
        {
            SeekerNumberButton seekerNumberButton = button.GetComponent<SeekerNumberButton>();

            buttonList.Add(seekerNumberButton);
        }
    }

    private void InitializeButtonState()
    {
        DeselectAllButton();
        buttonList[0].Select();
    }

    public void DeselectAllButton()
    {
        foreach (var button in buttonList)
        {
            button.Deselect();
        }
    }
}