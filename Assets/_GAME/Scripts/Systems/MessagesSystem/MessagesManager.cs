using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class MessagesManager : GameSystemComponent
{
    public static MessagesManager Instance { get; private set; }

    [SerializeField] private AppData appData;
    [SerializeField] private List<MessageData> initialMessages = new List<MessageData>();

    [ShowInInspector]
    private static List<MessageContactData> contacts;
    public static List<MessageContactData> Contacts 
    {
        get
        {
            if (contacts == null)
                contacts = Resources.LoadAll<MessageContactData>("Messages").ToList();

            return contacts;
        }
    }

    private static List<MessageData> allMessages;
    public static List<MessageData> AllMessages
    {
        get
        {
            if (allMessages == null)
                allMessages = Resources.LoadAll<MessageData>("Messages").ToList();

            return allMessages;
        }
    }

    private List<MessageInfoStack> MessageInfoStacks => PlayerController.PlayerInfo.messageStacks;

    public MessageContactData GetContactByID(string contactID)
    {
        for (int i = 0; i < Contacts.Count; i++)
        {
            if(contactID == Contacts[i].contactID)
                return Contacts[i];
        }

        return null;
    }

    public MessageData GetMessageDataByID(string messageID)
    {
        for (int i = 0; i < AllMessages.Count; i++)
        {
            if (messageID == AllMessages[i].messageID)
                return AllMessages[i];
        }

        return null;
    }

    public MessageInfoStack GetMessageInfoStackByContactID(string contactID, bool createNew = true)
    {
        for (int i = 0; i < MessageInfoStacks.Count; i++)
        {
            if (contactID == MessageInfoStacks[i].contactID)
                return MessageInfoStacks[i];
        }

        if (createNew == false)
            return null;

        MessageInfoStack newMessagesStack = new MessageInfoStack(contactID);
        MessageInfoStacks.Add(newMessagesStack);
        return newMessagesStack;
    }

    public void RecieveMessage(MessageData messageData, bool currentDateTime)
    {
        MessageInfoStack messagesStack = GetMessageInfoStackByContactID(messageData.contactID);
        MessageInfo newMessage = new MessageInfo(messageData, currentDateTime);
        messagesStack.messages.Add(newMessage);

        MessageInfoStacks.Remove(messagesStack);
        MessageInfoStacks.Insert(0, messagesStack);

        if (currentDateTime || (currentDateTime == false && messageData.unread))
            messagesStack.unreadMessagesCount++;

        if (currentDateTime)
            SpawnNotification(messageData);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ReorderInitialMessages();
        LoadInitialMessages();
    }

    private void ReorderInitialMessages()
    {
        initialMessages = initialMessages.OrderBy(x => x.DateTime).ToList();
    }

    private void LoadInitialMessages()
    {
        for (int i = 0; i < initialMessages.Count; i++)
        {
            RecieveMessage(initialMessages[i], false);
        }
    }

    private void SpawnNotification(MessageData messageData)
    {
        string notificationText = $"<b>{GetContactByID(messageData.contactID).contactName}</b> {messageData.messageText}";

        NotificationsOverlayUI_NotificationElement notification
            = NotificationsOverlayUI.Instance.ShowNotification(appData.appName, notificationText, appData.appIcon);

        notification.OnClick.AddListener(() =>
        {
            ClickSMSNotification(messageData.contactID);
        });
    }

    private void ClickSMSNotification(string contactID)
    {
        PhoneSimulatorManager.Instance.OpenApp(appData.ID);

        MessageInfoStack messageStack = GetMessageInfoStackByContactID(contactID, false);

        PhoneSimulatorManager.AppReferences appRef = PhoneSimulatorManager.Instance.GetAppReferencesByID(appData.ID);
        App_SMS appSMS;

        if (appRef == null)
            return;

        if (appRef.appController == null)
            return;

        if (appRef.appController.GetType() != typeof(App_SMS))
            return;

        appSMS = (App_SMS)appRef.appController;

        if (messageStack != null)
            appSMS.ShowMessageStack(messageStack);
        else
            appSMS.ShowAppView(0);
    }
}
