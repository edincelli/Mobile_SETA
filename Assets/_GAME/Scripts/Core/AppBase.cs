using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppBase : MonoBehaviour
{
    public List<AppView> appViews = new List<AppView>();

    [FoldoutGroup("Events")] public UnityEvent OnShowApp;
    [FoldoutGroup("Events")] public UnityEvent OnHideApp;
    [FoldoutGroup("Events")] public UnityEvent OnCloseApp;
    [FoldoutGroup("Events")] public UnityEvent OnBack;

    private int lastView = 0;

    public bool IsActive => gameObject.activeSelf;

    public virtual void ShowApp()
    {
        OnShowApp.Invoke();

        if (appViews.Count == 0)
            return;

        ShowAppView(appViews[lastView]);
    }

    public virtual void Back()
    {
        OnBack.Invoke();

        if(appViews.Count == 0)
        {
            PhoneSimulatorManager.Instance.ShowMainScreen();
            return;
        }

        if(lastView == 0)
        {
            PhoneSimulatorManager.Instance.ShowMainScreen();
            return;
        }

        bool wentBack = false;

        appViews[lastView].Back(out wentBack);

        if (wentBack)
            return;

    }

    public virtual void HideApp()
    {
        OnHideApp.Invoke();
    }

    public virtual void CloseApp()
    {
        OnCloseApp.Invoke();
        lastView = 0;
    }

    public virtual void ShowAppView(AppView selectedAppView)
    {
        HideAllScreens();
        lastView = appViews.IndexOf(selectedAppView);
        selectedAppView.ShowView();
    }

    public virtual void ShowAppView(int index)
    {
        if (index < 0 || index >= appViews.Count)
            return;

        ShowAppView(appViews[index]);
    }

    protected virtual void HideAllScreens()
    {
        for (int i = 0; i < appViews.Count; i++)
        {
            appViews[i].HideView();
        }
    }

    protected virtual void Awake()
    {
        for (int i = 0; i < appViews.Count; i++)
        {
            appViews[i].SetupView(this);
        }
    }

    protected virtual void Start()
    {

    }
}
