using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Messages/Message Data")]
public class MessageData : ScriptableObject, ISerializationCallbackReceiver
{
    public string messageID => name;
    [ListToPopup(typeof(MessageData), "TempList", "Required Dialogue")]
    public string contactID;
    [TextArea]
    public string dateTime = "5 / 25 / 2024<br>03:56 PM";
    [TextArea]
    public string messageText;
    public bool unread;
    public bool userMessage;

    //list variables
    public static List<string> TempList;
    [HideInInspector, NonSerialized] public List<string> contactList;

    public DateTime DateTime
    {
        get
        {
            string tempDateTimeString = dateTime.Replace(" ", "");
            tempDateTimeString = tempDateTimeString.Replace("<br>", " ");
            tempDateTimeString = tempDateTimeString.Replace("</br>", " ");
            tempDateTimeString = tempDateTimeString.Replace("\n", " ");

            if (tempDateTimeString.Length > 2)
                tempDateTimeString = tempDateTimeString.Insert(tempDateTimeString.Length - 2, " ");

            string dateString = System.DateTime.Now.ToString("M / d / yyyy", CultureInfo.GetCultureInfo("en-US"));
            string timeString = System.DateTime.Now.ToString("hh:mm tt", CultureInfo.GetCultureInfo("en-US"));

            try
            {
                DateTime parsedDateTime = DateTime.ParseExact(tempDateTimeString, "M/d/yyyy hh:mm tt", CultureInfo.GetCultureInfo("en-US"));
                return parsedDateTime;
            }
            catch (Exception ex)
            {
                Debug.LogError($"{messageID} has broken date!\n" +
                    $"dateTime:{dateTime}\n" +
                    $"tempDateTimeString:{tempDateTimeString}\n\n" +
                    $"{ex.Message}");

                return DateTime.Now;
            }
        }
    }

    public void OnAfterDeserialize() { }

    public void OnBeforeSerialize()
    {
        TempList = contactList;
    }
}
