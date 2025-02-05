using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NotificationsOverlayUI
{
    [Space]
    [Header("Popup Animation Settings")]
    [SerializeField] private RectTransform popupElement;

    [SerializeField] private float popupHiddenPosition = 300;
    [Space]
    [SerializeField, Range(float.Epsilon, 10)] private float popupChangeTime = 1;
    [SerializeField] private AnimationCurve popupChangeCurve;

    [Space]
    [ShowInInspector] private bool popupTestMode = false;

    private float currentVisibilityPopup = 0;
    private float initalPopupY;

    [ShowIf("@this.popupTestMode"), Range(0, 1), ShowInInspector]
    private float targetVisibilityPopup = 0;

    private void AwakePopup()
    {
        initalPopupY = popupElement.anchoredPosition.y;
    }

    private void SetPopupTargetVisibility(float value)
    {
        targetVisibilityPopup = value;
    }

    private void ChangePopupVisibilityOnClick()
    {
        currentVisibilityPopup = 1;
        ApplyPopupVisibilitySettings(1);
    }

    private void UpdatePopupVisibility()
    {
        if (currentVisibilityPopup == targetVisibilityPopup)
        {
            if (currentVisibilityPopup == 1)
            {
                currentVisibilityPopup = 0;
                targetVisibilityPopup = 0;
            }

            return;
        }

        currentVisibilityPopup = Mathf.MoveTowards(currentVisibilityPopup, targetVisibilityPopup, Time.deltaTime / popupChangeTime);

        ApplyPopupVisibilitySettings(currentVisibilityPopup);
    }

    private void ApplyPopupVisibilitySettings(float visibility)
    {
        float valueForPosition = popupChangeCurve.Evaluate(visibility);
        popupElement.anchoredPosition = new Vector2(popupElement.anchoredPosition.x, (1 - valueForPosition) * popupHiddenPosition + initalPopupY);
    }
}
