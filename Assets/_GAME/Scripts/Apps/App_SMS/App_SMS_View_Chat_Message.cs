using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class App_SMS_View_Chat_Message : MonoBehaviour
{
    [SerializeField] private Color userMessageColor;
    [SerializeField] private Color contactMessageColor;
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;

    [Space]
    [SerializeField] private Image outlineImage;
    [SerializeField] private TextMeshProUGUI contentTMP;

    private MessageInfo messageInfo;

    public void SetupMessage(MessageInfo messageInfo)
    {
        this.messageInfo = messageInfo;

        contentTMP.text = messageInfo.messageText;

        if (messageInfo.userMessage)
        {
            verticalLayoutGroup.childAlignment = TextAnchor.UpperRight;
            outlineImage.color = userMessageColor;
        }
        else
        {
            verticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            outlineImage.color = contactMessageColor;
        }
    }

    public void ClickMessage()
    {
        MessageData messageData = MessagesManager.Instance.GetMessageDataByID(messageInfo.messageID);

        if (messageData == null)
            return;

        ScenarioManager.Instance.CheckObjectInRequirements(messageData);
    }
}
