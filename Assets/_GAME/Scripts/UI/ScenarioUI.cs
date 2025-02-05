using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioUI : MonoBehaviour
{
    public static ScenarioUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI taskTMP;
    [SerializeField] private Button buttonContinue;
    [SerializeField] private TextMeshProUGUI buttonTMP;
    [SerializeField] private Image buttonProgressBar;

    [Space]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform elementsParent;
    [SerializeField] private GameObject elementTextPrefab;
    [SerializeField] private GameObject elementImagePrefab;

    [Space]
    [SerializeField] private Color errorMessageColor;
    [SerializeField] private Color infoMessageColor;

    [Space]
    [Header("Animation Settings")]
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float targetWidth = 1000;
    [Space]
    [SerializeField, Range(0, 1)] private float waitPercent = 0.3f;
    [SerializeField, Range(0, 1)] private float fullWidthPercent = 0.75f;
    [SerializeField, Range(0, 1)] private float alphaZeroPercent = 0.7f;
    [Space]
    [SerializeField, Range(float.Epsilon, 5)] private float changeTime = 1;
    [SerializeField] private AnimationCurve changeCurve;

    [Space]
    [SerializeField] private LayoutElement connectedLayoutElement;
    [SerializeField, ShowIf("@this.connectedLayoutElement != null")] private float targetWidthConnected = 250;

    [Space]
    [ShowInInspector] private bool testMode = false;

    private float currentVisibility = 0;

    [ShowIf("@this.testMode"), Range(0,1), ShowInInspector]
    private float targetVisibility = 0;

    public bool ScrolledDown
    {
        get
        {
            if (scrollRect.verticalScrollbar.isActiveAndEnabled == false)
                return true;

            return scrollRect.verticalNormalizedPosition <= 0.1f;
        }
    }

    public void ShowStage(ScenarioStage stage)
    {
        ClearContent();

        if(stage == null)
        {
            SetTargetVisibility(0, true);
            return;
        }

        for (int i = 0; i < stage.ScenarioElements.Count; i++)
        {
            ScenarioElement element = stage.ScenarioElements[i];

            switch (element.TypeElement)
            {
                case ScenarioElement.ElementType.Text:
                    TextMeshProUGUI newText = Instantiate(elementTextPrefab, elementsParent).GetComponent<TextMeshProUGUI>();
                    newText.text = element.Text;
                    break;
                case ScenarioElement.ElementType.Picture:
                    Image newImage = Instantiate(elementImagePrefab, elementsParent).GetComponent<Image>();
                    newImage.sprite = element.Sprite;
                    newImage.rectTransform.sizeDelta = new Vector2(newImage.rectTransform.sizeDelta.x, element.Height);
                    break;
                default:
                    break;
            }
        }

        buttonTMP.text = stage.ButtonText;
        scrollRect.verticalNormalizedPosition = 1;

        elementsParent.GetComponent<UILayoutRebuilder>().ForceRebuild();
    }

    public void UpdateButton(bool interactable)
    {
        buttonContinue.interactable = interactable;
    }

    public void ClickScenarioButton()
    {
        ScenarioManager.Instance.PrepareNextStage();
    }

    public void SetTargetVisibility(int visibility, bool skipWaiting)
    {
        targetVisibility = visibility;

        if (skipWaiting && currentVisibility < waitPercent)
            currentVisibility = waitPercent;
    }

    public void ShowErrorMessage(string message)
    {
        ShowTaskMessage(message);
        taskTMP.color = errorMessageColor;
    }

    public void ShowInfoMessage(string message)
    {
        ShowTaskMessage(message);
        taskTMP.color = infoMessageColor;
    }

    public void ClearTaskMessage()
    {
        ShowTaskMessage("");
    }

    private void Awake()
    {
        testMode = false;
        Instance = this;
    }

    private void Start()
    {
        ApplyVisibilitySettings(targetVisibility);
        currentVisibility = targetVisibility;
        buttonTMP.text = "Start";
    }

    private void Update()
    {
        UpdateVisibility();
        UpdateButtonProgressBar();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetTargetVisibility(0, true);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetTargetVisibility(1, true);
    }

    private void UpdateVisibility()
    {
        if (currentVisibility == targetVisibility)
            return;

        currentVisibility = Mathf.MoveTowards(currentVisibility, targetVisibility, Time.deltaTime / changeTime);

        ApplyVisibilitySettings(currentVisibility);
    }

    private void UpdateButtonProgressBar()
    {
        buttonProgressBar.fillAmount = ScenarioManager.Instance.CurrentStageRequirementsProgress;
    }

    private void ApplyVisibilitySettings(float visibility)
    {
        float valueForWidth = changeCurve.Evaluate(visibility.PercentInRange(waitPercent, fullWidthPercent));
        float valueForAlpha = changeCurve.Evaluate(visibility.PercentInRange(alphaZeroPercent, 1f));

        layoutElement.preferredWidth = valueForWidth * targetWidth;
        canvasGroup.alpha = valueForAlpha;

        if(connectedLayoutElement != null)
        {
            connectedLayoutElement.preferredWidth = valueForWidth * targetWidthConnected;
        }

        if (canvasGroup.alpha < 0.1f)
            canvasGroup.blocksRaycasts = false;
        else
            canvasGroup.blocksRaycasts = true;
    }

    private void ClearContent()
    {
        for (int i = elementsParent.childCount - 1; i >= 0; i--)
        {
            Destroy(elementsParent.GetChild(i).gameObject);
        }

        taskTMP.text = "";
        buttonContinue.interactable = false;
    }


    public void ShowTaskMessage(string taskMessage)
    {
        taskTMP.text = taskMessage;

        if (string.IsNullOrEmpty(taskMessage) == false)
            SetTargetVisibility(1, true);
    }
}
