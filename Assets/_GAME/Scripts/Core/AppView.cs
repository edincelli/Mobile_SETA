using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppView : MonoBehaviour
{
    [SerializeField] private AppView parentView;

    [FoldoutGroup("Events")] public UnityEvent OnShowView;
    [FoldoutGroup("Events")] public UnityEvent OnHideView;
    [FoldoutGroup("Events")] public UnityEvent OnBack;

    private AppBase app;

    public virtual void ShowView()
    {
        if (gameObject.activeSelf == true)
            return;

        OnShowView.Invoke();
        gameObject.SetActive(true);
    }

    public virtual void HideView()
    {
        if (gameObject.activeSelf == false)
            return;

        OnHideView.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void Back(out bool hasParentView)
    {
        OnBack.Invoke();

        if (parentView == null)
        {
            hasParentView = false;
            return;
        }

        hasParentView = true;
        app.ShowAppView(parentView);
    }

    public virtual void SetupView(AppBase app)
    {
        this.app = app;
    }
}
