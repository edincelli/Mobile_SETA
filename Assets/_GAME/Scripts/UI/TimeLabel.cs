using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TimeLabel : MonoBehaviour
{
    [SerializeField] private Format format;
    [SerializeField] private TextMeshProUGUI timeTMP;

    void Update()
    {
        switch (format)
        {
            case Format.JUST_TIME:
                timeTMP.text = System.DateTime.Now.ToString("hh:mm tt", CultureInfo.GetCultureInfo("en-US"));
                break;
            case Format.DATE_AND_TIME:
                string dateString = System.DateTime.Now.ToString("dddd, MMMM dd", CultureInfo.GetCultureInfo("en-US"));
                string timeString = System.DateTime.Now.ToString("hh:mm", CultureInfo.GetCultureInfo("en-US"));
                timeTMP.text = $"<size=40%>{dateString}</size><br>{timeString}";
                break;
            default:
                break;
        }

    }

    public enum Format
    {
        JUST_TIME,
        DATE_AND_TIME
    }
}
