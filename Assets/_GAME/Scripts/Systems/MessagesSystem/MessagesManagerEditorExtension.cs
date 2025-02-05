using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class MessagesManager
{
#if UNITY_EDITOR
    private bool importerContactsVisible = false;
    private bool importerMessagesVisible = false;

    [ShowIf("@this.importerContactsVisible"), Space]
    [SerializeField, TextArea(10, 20)] private string contactsJSON;

    [ShowIf("@this.importerMessagesVisible"), Space]
    [SerializeField, TextArea(10, 20)] private string messagesJSON;

    [Button, HideIf("@this.importerContactsVisible || this.importerMessagesVisible"), PropertySpace]
    private void ShowContactsImporter()
    {
        importerContactsVisible = true;
    }

    [Button, HideIf("@this.importerMessagesVisible || this.importerContactsVisible")]
    private void ShowMessagesImporter()
    {
        importerMessagesVisible = true;
    }

    [Button, ShowIf("@this.importerContactsVisible")]
    private void HideContactsImporter()
    {
        importerContactsVisible = false;
        contactsJSON = "";
    }

    [Button, ShowIf("@this.importerMessagesVisible")]
    private void HideMessagesImporter()
    {
        importerMessagesVisible = false;
        messagesJSON = "";
    }

    [Button, ShowIf("@this.importerContactsVisible")]
    private void ImportContacts()
    {
        List<TempContactImporterClass> tempContacts = ParseListFromJson<TempContactImporterClass, ContactList>(contactsJSON);
        Debug.Log(string.Join("\n", tempContacts));

        for (int i = 0; i < tempContacts.Count; i++)
        {
            MessageContactData newContact = ScriptableObject.CreateInstance<MessageContactData>();
            newContact.contactName = tempContacts[i].contactName;
            newContact.contactPhoneNumber = tempContacts[i].contactPhoneNumber;

            AssetDatabase.CreateAsset(newContact, $"Assets/_GAME/Resources/Messages/Contacts/{tempContacts[i].contactID}.asset");
            AssetDatabase.SaveAssets();
        }

        HideContactsImporter();
    }

    [Button, ShowIf("@this.importerMessagesVisible")]
    private void ImportMessages()
    {
        List<TempMessageImporterClass> tempMessages = ParseListFromJson<TempMessageImporterClass, MessagesList>(messagesJSON);
        Debug.Log(string.Join("\n", tempMessages));

        for (int i = 0; i < tempMessages.Count; i++)
        {
            MessageData newMessage = ScriptableObject.CreateInstance<MessageData>();
            newMessage.contactID = tempMessages[i].contactID;
            newMessage.dateTime = tempMessages[i].dateTime;
            newMessage.messageText = tempMessages[i].messageText;
            newMessage.unread = tempMessages[i].unread;
            newMessage.userMessage = tempMessages[i].userMessage;

            string messageFolderPath = $"Assets/_GAME/Resources/Messages/Messages/Messages_{newMessage.contactID}";

            if(Directory.Exists(messageFolderPath) == false)
                Directory.CreateDirectory(messageFolderPath);

            AssetDatabase.CreateAsset(newMessage, $"{messageFolderPath}/{tempMessages[i].messageID}.asset");
            AssetDatabase.SaveAssets();
        }

        HideMessagesImporter();
    }

    private static List<T> ParseListFromJson<T, T2>(string json) where T2 : IListContainer<T>
    {
        try
        {
            T2 container = JsonConvert.DeserializeObject<T2>(json);
            return container?.GetItems() ?? new List<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
            return new List<T>();
        }
    }

    private interface IListContainer<T>
    {
        List<T> GetItems();
    }

    private class ContactList : IListContainer<TempContactImporterClass>
    {
        public List<TempContactImporterClass> contacts { get; set; }

        public List<TempContactImporterClass> GetItems()
        {
            return contacts;
        }
    }

    private class MessagesList : IListContainer<TempMessageImporterClass>
    {
        public List<TempMessageImporterClass> messages { get; set; }

        public List<TempMessageImporterClass> GetItems()
        {
            return messages;
        }
    }

    private class TempContactImporterClass
    {
        public string contactID;
        public string contactName;
        public string contactPhoneNumber;

        public override string ToString()
        {
            return $"{contactID}: {contactName} - {contactPhoneNumber}";
        }
    }

    private class TempMessageImporterClass
    {
        public string messageID;
        public string contactID;
        public string dateTime;
        public string messageText;
        public bool unread;
        public bool userMessage;

        public override string ToString()
        {
            return $"{messageID}: {contactID}, {dateTime}, {messageText}, unread:{unread}";
        }
    }
#endif
}
