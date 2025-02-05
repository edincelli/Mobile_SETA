using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class CallOverlayUI
{
    [Space]
    [Header("Animation Settings")]
    [SerializeField] private CanvasGroup canvasGroup;
    [Space]
    [SerializeField, Range(float.Epsilon, 5)] private float changeTime = 1;
    [SerializeField] private AnimationCurve changeCurve;

    [Space]
    [ShowInInspector] private bool testMode = false;

    private float currentVisibility = 0;

    [ShowIf("@this.testMode"), Range(0, 1), ShowInInspector]
    private float targetVisibility = 0;

    private void SetTargetVisibility(float value)
    {
        targetVisibility = value;
    }

    private void UpdateVisibility()
    {
        if (currentVisibility == targetVisibility)
            return;

        currentVisibility = Mathf.MoveTowards(currentVisibility, targetVisibility, Time.deltaTime / changeTime);

        ApplyVisibilitySettings(currentVisibility);
    }

    private void ApplyVisibilitySettings(float visibility)
    {
        float valueForAlpha = changeCurve.Evaluate(Mathf.Clamp01(visibility));
        canvasGroup.alpha = valueForAlpha;

        if (canvasGroup.alpha < 0.9f)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
