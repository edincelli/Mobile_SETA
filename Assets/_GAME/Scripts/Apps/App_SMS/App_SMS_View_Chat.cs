using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class App_SMS_View_Chat : AppView
{
    [SerializeField] private Transform messagesParent;
    [SerializeField] private GameObject messagePrefab;

    [Space]
    [SerializeField] private TextMeshProUGUI contactName;
    [SerializeField] private Image contactImage;
    [SerializeField] private UILayoutRebuilder layoutRebuilder;

    private MessageInfoStack messageStack;

    public void SetupView(MessageInfoStack messageStack)
    {
        this.messageStack = messageStack;

        contactName.text = messageStack.ContactName;

        Sprite contactPicture = messageStack.ContactPicture;

        if (contactPicture != null)
            contactImage.sprite = contactPicture;

        UpdateMessages();
        ScenarioManager.Instance.CheckObjectInRequirements(messageStack.Contact);
    }

    private void UpdateMessages()
    {
        int currentButtonsCount = messagesParent.childCount;

        for (int i = 0; i < messageStack.messages.Count; i++)
        {
            int tempI = i;
            App_SMS_View_Chat_Message button;

            if (i >= currentButtonsCount)
            {
                Instantiate(messagePrefab, messagesParent);
            }

            button = messagesParent.GetChild(i).GetComponent<App_SMS_View_Chat_Message>();
            button.SetupMessage(messageStack.messages[tempI]);
        }

        for (int i = currentButtonsCount; i > messageStack.messages.Count; i--)
        {
            messagesParent.GetChild(i - 1).gameObject.Destroy();
        }

        layoutRebuilder.ForceRebuild();
    }
}
