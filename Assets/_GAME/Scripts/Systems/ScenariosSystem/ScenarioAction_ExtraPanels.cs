using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioAction_ExtraPanels : ScenarioAction
{
    private PhoneSimulatorManager Phone => PhoneSimulatorManager.Instance;

    public void ShowPausePanel()
    {
        Phone.ShowPausePanel();
    }

    public void HideAllPanels() 
    {
        Phone.HideAllExtraPanels();
    }
}
