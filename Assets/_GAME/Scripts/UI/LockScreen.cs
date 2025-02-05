using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField, Range(float.Epsilon, 5)] private float changeTime = 1;
    [SerializeField] private AnimationCurve changeCurve;

    private float currentVisibility = 0;
    private float targetVisibility = 1;

    public void Unlock()
    {
        PhoneSimulatorManager.Instance.Unlock();
        ScenarioUI.Instance.SetTargetVisibility(1, false);
        targetVisibility = 0;
    }

    private void Start()
    {
        ApplyVisibilitySettings(targetVisibility);
        currentVisibility = targetVisibility;
    }

    private void Update()
    {
        UpdateVisibility();
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
        float valueForAlpha = changeCurve.Evaluate(visibility);
        canvasGroup.alpha = valueForAlpha;

        if (canvasGroup.alpha < 0.1f)
            canvasGroup.blocksRaycasts = false;
        else
            canvasGroup.blocksRaycasts = true;
    }
}
