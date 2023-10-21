using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SpecialQuestTemplate")]
public class SpecialQuestTemplate : ScriptableObject
{
    [SerializeField] private List<SpecialQuest> _specialQuestList = new List<SpecialQuest>();

    public List<SpecialQuest> SpecialQuestList => _specialQuestList;
}