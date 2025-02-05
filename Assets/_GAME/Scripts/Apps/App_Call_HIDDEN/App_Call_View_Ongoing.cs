using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class App_Call_View_Ongoing : AppView
{
    [SerializeField] private App_Call appCall;
    [SerializeField] private TextMeshProUGUI contactNameTMP;
    [SerializeField] private TextMeshProUGUI numberTMP;
    [SerializeField] private Image contactPicure;
    [SerializeField] private TextMeshProUGUI timerTMP;

    public override void ShowView()
    {
        SetupView();
        base.ShowView();
    }

    public override void HideView()
    {
        base.HideView();
    }

    public void HangUpCall()
    {
        CallsManager.Instance.StopCall();
    }

    public void ShowMainScreen()
    {
        PhoneSimulatorManager.Instance.ShowMainScreen();
    }

    private void SetupView()
    {
        contactNameTMP.text = CallsManager.Instance.Contact.contactName;
        numberTMP.text = CallsManager.Instance.Contact.contactPhoneNumber;
        contactPicure.sprite = CallsManager.Instance.Contact.contactPhoto;
    }

    private void Update()
    {
        timerTMP.text = CallsManager.Instance.CallTimeString;
    }
}
