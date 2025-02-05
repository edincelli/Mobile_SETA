using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioAction_PhoneSimulator : MonoBehaviour
{
    [SerializeField] private AppData appToOpen;

    private PhoneSimulatorManager Phone => PhoneSimulatorManager.Instance;

    public void OpenApp()
    {
        if (appToOpen == null)
        {
            Debug.LogError("appToOpen is null", transform);
            return;
        }

        Phone.OpenApp(appToOpen.ID);
    }

    public void Back()
    {
        Phone.Back();
    }

    public void ShowMainScreen()
    {
        Phone.ShowMainScreen();
    }

    public void ShowAppsInBackground()
    {
        Phone.ShowAppsInBackground();
    }

    public void RemoveAllAppsFromBackground()
    {
        Phone.RemoveAllAppsFromBackground();
    }
}
