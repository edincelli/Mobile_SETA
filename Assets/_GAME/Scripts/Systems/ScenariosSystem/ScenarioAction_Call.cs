using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioAction_Call : ScenarioAction
{
    [SerializeField] private CallScript callScript;

    public void SetupCall()
    {
        CallsManager.Instance.SetupCall(callScript);
    }

    public void WaitWithNextStage()
    {
        CallsManager.WaitBeforeStartingNextStage = true;
    }

    public void ContinueWithNextStage()
    {
        CallsManager.WaitBeforeStartingNextStage = false;
    }

    public void ShowCallApp()
    {
        CallOverlayUI.Instance.ClickCallOverlay();
    }

    public void BlockHangUp()
    {
        CallsManager.CanHangUp = false;
    }

    public void AllowHangUp()
    {
        CallsManager.CanHangUp = true;
    }
}
