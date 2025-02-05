using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class App_SMS : AppBase
{
    [Space]
    [SerializeField] private App_SMS_View_Main smsMainView;
    [SerializeField] private App_SMS_View_Chat smsChatView;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void ShowMessageStack(MessageInfoStack messageStack)
    {
        ShowAppView(1);
        smsChatView.SetupView(messageStack);
        messageStack.unreadMessagesCount = 0;
    }
}
