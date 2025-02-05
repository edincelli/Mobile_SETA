using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class NotificationsOverlayUI
{
    [Space]
    [Header("Animation Settings")]
    [SerializeField] private RectTransform scrollableElement;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float hiddenPosition = 900;
    [Space]
    [SerializeField, Range(0, 1)] private float zeroPositionPercent = 0.4f;
    [SerializeField, Range(0, 1)] private float alphaFullPercent = 0.7f;
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
        float valueForWidth = changeCurve.Evaluate(visibility.PercentInRange(zeroPositionPercent, 1f));
        float valueForAlpha = changeCurve.Evaluate(visibility.PercentInRange(0f, alphaFullPercent));

        scrollableElement.anchoredPosition = new Vector2(0, (1-valueForWidth) * hiddenPosition);
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
