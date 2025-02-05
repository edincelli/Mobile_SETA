using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class App_SMS_ChatPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI contentLabel;
    [SerializeField] private TextMeshProUGUI dateLabel;
    [SerializeField] private GameObject notificationObject;
    [SerializeField] private TextMeshProUGUI notificationLabel;
    [SerializeField] private Image contactImage;

    private App_SMS appSMS;
    private MessageInfoStack messageStack;

    public void SetupPanel(MessageInfoStack messageStack, App_SMS appSMS)
    {
        this.messageStack = messageStack;
        this.appSMS = appSMS;

        int unreadMessagesCount = messageStack.unreadMessagesCount;

        nameLabel.text = messageStack.ContactName;
        contentLabel.text = messageStack.LastMessageShort;
        dateLabel.text = messageStack.LastMessageDateTime;
        notificationObject.SetActive(unreadMessagesCount > 0);
        notificationLabel.text = unreadMessagesCount.ToString();

        Sprite contactPicture = messageStack.ContactPicture;

        if (contactImage != null)
            contactImage.sprite = contactPicture;
    }

    public void Click()
    {
        appSMS.ShowMessageStack(messageStack);
    }
}
