using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class QuestTitlePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _dayTimeText;

    private void OnEnable()
    {
        SetUpDayTimeText();
    }

    private void SetUpDayTimeText()
    {
        DateTime dateTime = DateTime.Today;
        var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        var dateTimeDayOfWeek = dateTime.DayOfWeek;
        var dateTimeDay = dateTime.Day;
        var dateTimeYear = dateTime.Year;
        Debug.Log($"DAYTIME TEST --- DAY OF WEEK: {dateTimeDayOfWeek} --- DAY: {dateTimeDay} --- MONTH: {month} --- YEAR: {dateTimeYear}");

        _dayTimeText.text = string.Format($"{dateTimeDayOfWeek}, {dateTimeDay} {month} {dateTimeYear}");
    }
}