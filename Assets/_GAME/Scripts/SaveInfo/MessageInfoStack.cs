using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MessageInfoStack
{
    public string contactID;
    public List<MessageInfo> messages = new List<MessageInfo>();
    public int unreadMessagesCount;

    public MessageContactData Contact => MessagesManager.Instance.GetContactByID(contactID);

    public string ContactName
    {
        get
        {
            if (MessagesManager.Instance == null)
                return "Unknown";

            if(Contact == null) 
                return "Unknown";

            return Contact.contactName;
        }
    }

    public Sprite ContactPicture
    {
        get
        {
            if (MessagesManager.Instance == null)
                return null;

            if (Contact == null)
                return null;

            return Contact.contactPhoto;
        }
    }

    public string LastMessageShort
    {
        get
        {
            if (messages.Count == 0)
                return "Empty";

            MessageInfo lastMessageObject = messages[messages.Count - 1];

            if (lastMessageObject == null)
                return "Empty";

            string lastMessage = messages[messages.Count - 1].messageText;

            if (string.IsNullOrEmpty(lastMessage))
                return "Empty";

            if (lastMessage.Length > 120)
                lastMessage = lastMessage.Substring(0, 117) + "...";

            if (unreadMessagesCount > 0)
                lastMessage = $"<b>{lastMessage}</b>";
            else if(lastMessageObject.userMessage)
                lastMessage = $"You: {lastMessage}";

            return lastMessage;
        }
    }

    public string LastMessageDateTime
    {
        get
        {
            if (messages.Count == 0)
                return "-- / -- / ----\n--:-- --";

            return messages[messages.Count - 1].dateTime;
        }
    }

    public MessageInfoStack(string contactID)
    {
        this.contactID = contactID;
    }
}
