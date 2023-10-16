using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/QuestTemplate")]
public class QuestTemplate : ScriptableObject
{
    [SerializeField] private List<Quest> _questList = new List<Quest>();

    public List<Quest> QuestList => _questList;
}