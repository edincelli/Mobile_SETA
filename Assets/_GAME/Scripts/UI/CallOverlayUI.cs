using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class CallOverlayUI : MonoBehaviour
{
    public static CallOverlayUI Instance { get; private set; }

    [Space]
    [SerializeField] private TextMeshProUGUI timerTMP;

    public static bool Active => Instance.currentVisibility > 0.9f;

    public void ShowCallOverlay()
    {
        SetTargetVisibility(1);
    }

    public void HideCallOverlay()
    {
        SetTargetVisibility(0);
    }

    public void ClickCallOverlay()
    {
        if (CallsManager.Instance.CallInProgress)
            CallsManager.Instance.ShowApp();
        else
            HideCallOverlay();
    }

    private void Awake()
    {
        Instance = this;
        SetTargetVisibility(0);
        ApplyVisibilitySettings(0);
    }
    private void Update()
    {
        UpdateVisibility();
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (targetVisibility == 0)
            return;
     
        timerTMP.text = CallsManager.Instance.CallTimeString;
    }
}
