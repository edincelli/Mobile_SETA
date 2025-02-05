using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.CompositeCollider2D;

public class CallTranscriptUI : MonoBehaviour
{
    public static CallTranscriptUI Instance { get; private set; }

    [SerializeField] private UILayoutRebuilder layoutRebuilder;
    [SerializeField] private TextMeshProUGUI backgroundTMP;
    [SerializeField] private TextMeshProUGUI animatedTMP;

    [Space]
    [Header("Text Settings")]
    [SerializeField, Range(float.Epsilon, 1f)] private float characterDelay = 0.1f;
    [SerializeField, Range(1, 5)] private int characterStep = 1;

    [Space]
    [Header("Animation Settings")]
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField, Range(float.Epsilon, 5)] private float changeTime = 1;
    [SerializeField] private AnimationCurve changeCurve;

    [Space]
    [ShowInInspector] private bool testMode = false;

    private float currentVisibility = 0;

    [ShowIf("@this.testMode"), Range(0, 1), ShowInInspector]
    private float targetVisibility = 0;

    private string textToShow = "";
    private float characterTimer = 0;

    public void ShowTranscript(string transcript)
    {
        backgroundTMP.text = transcript;
        animatedTMP.text = "";
        textToShow = transcript;
        layoutRebuilder.ForceRebuild();
        SetTargetVisibility(1);
    }

    public void HideCallTranscript()
    {
        SetTargetVisibility(0);
    }

    private void Awake()
    {
        Instance = this;
        SetTargetVisibility(0);
        ApplyVisibilitySettings(0);

        backgroundTMP.text = "";
        animatedTMP.text = "";
    }

    private void Update()
    {
        UpdateOutputText();
        UpdateVisibility();
    }

    private void UpdateOutputText()
    {
        if (textToShow.Length == 0)
            return;

        characterTimer += Time.deltaTime;

        if (characterTimer < characterDelay)
            return;

        characterTimer = 0;
        AppendGenerationText(textToShow.Substring(0, Mathf.Min(textToShow.Length, characterStep)));
        textToShow = textToShow.Substring(Mathf.Min(textToShow.Length, characterStep));
    }

    private void AppendGenerationText(string text)
    {
        animatedTMP.text += text;
    }

    #region CanvasAnimation
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
    #endregion
}
