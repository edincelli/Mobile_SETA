using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[Serializable]
public class MessageInfo
{
    public string messageID;
    public string messageText;
    public string dateTime;
    public bool userMessage;

    public MessageInfo(MessageData messageData, bool currentDateTime)
    {
        this.messageID = messageData.messageID;
        this.messageText = messageData.messageText;
        this.userMessage = messageData.userMessage;

        if (currentDateTime)
        {
            string dateString = System.DateTime.Now.ToString("M / d / yyyy", CultureInfo.GetCultureInfo("en-US"));
            string timeString = System.DateTime.Now.ToString("hh:mm tt", CultureInfo.GetCultureInfo("en-US"));
            dateTime = $"{dateString}<br>{timeString}";
        }
        else
        {
            dateTime = messageData.dateTime;
        }
    }
}
