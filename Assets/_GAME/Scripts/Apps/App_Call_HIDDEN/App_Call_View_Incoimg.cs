using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class App_Call_View_Incoimg : AppView
{
    [SerializeField] private App_Call appCall;
    [SerializeField] private TextMeshProUGUI contactNameTMP;
    [SerializeField] private TextMeshProUGUI numberTMP;
    [SerializeField] private Image contactPicure;

    public override void ShowView()
    {
        SetupView();
        PhoneSimulatorManager.LockMainButtons = true;
        base.ShowView();
    }

    public override void HideView()
    {
        PhoneSimulatorManager.LockMainButtons = false;
        base.HideView();
    }

    public void AcceptCall()
    {
        CallsManager.Instance.AcceptCall();
    }

    public void DeclineCall()
    {
        CallsManager.Instance.StopCall();
    }

    private void SetupView()
    {
        contactNameTMP.text = CallsManager.Instance.Contact.contactName;
        numberTMP.text = CallsManager.Instance.Contact.contactPhoneNumber;
        contactPicure.sprite = CallsManager.Instance.Contact.contactPhoto;
    }
}
