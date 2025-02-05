using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PhoneSimulatorManager
{
    [Header("Apps Settings")]
    [SerializeField] private Transform viewsParent;
    [SerializeField] private AppsInBackgroundView appsInBackgroundView;

    private List<AppReferences> apps = new List<AppReferences>();

    private List<string> appsInBackground = new List<string>();
    private string activeAppID;

    public List<string> AppsInBackground => appsInBackground;


    public void OpenApp(string appID)
    {
        AppReferences appReferences = GetAppReferencesByID(appID);

        if (appReferences == null)
            return;

        if (appReferences.appGameObject == null)
            return;

        MoveCurrentAppToBackground();

        appReferences.appGameObject.SetActive(true);
        activeAppID = appID;
        RemoveAppFromBackground(activeAppID);

        if (appReferences.appController != null)
            appReferences.appController.ShowApp();
    }

    public void MoveCurrentAppToBackground()
    {
        AppReferences appReferences = GetAppReferencesByID(activeAppID);

        if (appReferences == null)
            return;

        if (appReferences.appGameObject != null)
            appReferences.appGameObject.SetActive(false);

        if (appReferences.appController != null)
            appReferences.appController.HideApp();

        if (appReferences.appData.hiddenApp == false)
            appsInBackground.Add(activeAppID);

        activeAppID = "-";
    }

    public void RemoveAppFromBackground(string appID)
    {
        appsInBackground.Remove(appID);

        if (appsInBackground.Count == 0)
            appsInBackgroundView.SetupView();

        AppReferences appReferences = GetAppReferencesByID(appID);

        if (appReferences != null)
        {
            if (appReferences.appController != null)
            {
                appReferences.appController.CloseApp();
            }
        }
    }

    public void RemoveAllAppsFromBackground()
    {
        for (int i = 0; i < appsInBackground.Count; i++)
        {
            AppReferences appReferences = GetAppReferencesByID(appsInBackground[i]);

            if (appReferences == null)
                continue;

            if (appReferences.appController == null)
                continue;

            appReferences.appController.CloseApp();
        }

        AppsInBackground.Clear();
        ShowMainScreen();
    }

    public AppReferences GetAppReferencesByID(string appID)
    {
        for (int i = 0; i < apps.Count; i++)
        {
            if (apps[i].appID == appID)
                return apps[i];
        }

        return null;
    }

    public AppBase GetAppControllerByID(string appID)
    {
        AppReferences appReferences = GetAppReferencesByID(appID);

        if (appReferences == null)
            return null;

        if (appReferences.appController == null)
            return null;

        return appReferences.appController;
    }

    private void AwakeApps()
    {
        LoadApps();
        PrepareApps();
    }

    private void LoadApps()
    {
        List<AppData> appList = new List<AppData>(Resources.LoadAll<AppData>("Apps"));

        List<AppData> appListHidden = new List<AppData>(Resources.LoadAll<AppData>("Apps_Hidden"));

        if (appListHidden.Count > 0)
            appList.AddRange(appListHidden);

        for (int i = 0; i < appList.Count; i++)
        {
            AppReferences newApp = new AppReferences();
            newApp.appID = appList[i].ID;
            newApp.appData = appList[i];
            apps.Add(newApp);
        }
    }

    private void PrepareApps()
    {
        for (int i = 0; i < apps.Count; i++)
        {
            if (apps[i].appData.appPrefab == null)
                continue;

            GameObject view = Instantiate(apps[i].appData.appPrefab, viewsParent);
            view.SetActive(false);
            apps[i].appGameObject = view;

            AppBase appController = view.GetComponent<AppBase>();
            if (appController != null)
                apps[i].appController = appController;
        }
    }

    public class AppReferences
    {
        public string appID;
        public AppData appData;
        public AppBase appController;
        public GameObject appGameObject;
    }
}
