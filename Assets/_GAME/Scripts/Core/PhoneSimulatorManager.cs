using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PhoneSimulatorManager : MonoBehaviour
{
    public static PhoneSimulatorManager Instance { get; private set; }

    public static bool IsUnlocked { get; private set; } = false;
    public static bool LockMainButtons { get; set; } = false;


    public void Back()
    {
        if (IsAnyExtraPanelActive)
            return;

        if (IsUnlocked == false)
            return;

        if (LockMainButtons)
            return;

        if (NotificationsOverlayUI.Active)
        {
            NotificationsOverlayUI.Instance.HideNotificationsOverlay();
            return;
        }

        if (appsInBackgroundView.gameObject.activeSelf)
        {
            ShowMainScreen();
            return;
        }
        
        AppReferences activeAppReferences = GetAppReferencesByID(activeAppID);

        if(activeAppReferences == null)
        {
            ShowMainScreen();
            return;
        }

        if (activeAppReferences.appController == null)
        {
            ShowMainScreen();
            return;
        }

        activeAppReferences.appController.Back();
    }

    public void ShowMainScreen()
    {
        if (IsAnyExtraPanelActive)
            return;

        if (IsUnlocked == false)
            return;

        if (LockMainButtons)
            return;

        if (NotificationsOverlayUI.Active)
            NotificationsOverlayUI.Instance.HideNotificationsOverlay();

        MoveCurrentAppToBackground();
        appsInBackgroundView.gameObject.SetActive(false);
    }

    public void ShowAppsInBackground()
    {
        if (IsAnyExtraPanelActive)
            return;

        if (IsUnlocked == false)
            return;

        if (LockMainButtons)
            return;

        if (NotificationsOverlayUI.Active)
            NotificationsOverlayUI.Instance.HideNotificationsOverlay();

        MoveCurrentAppToBackground();
        appsInBackgroundView.gameObject.SetActive(true);
        appsInBackgroundView.SetupView();
    }

    public void Unlock()
    {
        IsUnlocked = true;
    }

    private void Awake()
    {
        Instance = this;
        AwakeApps();
        AwakePanels();
    }

    private void Start()
    {
        ShowMainScreen();
    }
}
