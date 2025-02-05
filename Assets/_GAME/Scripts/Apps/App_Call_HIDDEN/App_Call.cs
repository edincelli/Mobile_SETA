using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App_Call : AppBase
{
    [Space]
    [SerializeField] private App_Call_View_Incoimg callViewIncoming;
    [SerializeField] private App_Call_View_Ongoing callViewOngoing;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void HideAllScreens()
    {
        base.HideAllScreens();
    }

    public override void ShowApp()
    {
        CallsManager.Instance.ShowHideOverlay();
        base.ShowApp();
    }

    public override void HideApp()
    {
        CallsManager.Instance.ShowHideOverlay();
        base.HideApp();
    }

    public void ShowIncomingCall()
    {
        ShowAppView(1);
    }

    public void ShowOngoinCall()
    {
        ShowAppView(0);
    }
}
