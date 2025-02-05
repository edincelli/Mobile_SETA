using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private Scenario selectedScenraio;

    [Space]
    [SerializeField] private string headerCongrats = "Congratulations!";
    [SerializeField] private string contentCongrats = "You have just finished this scenario!";
    [SerializeField, PreviewField] private Sprite spriteCongrats;

    private int currentStageIndex = 0;

    public static ScenarioManager Instance { get; private set; }

    public ScenarioStage CurrentStage { get; private set; }
    public float CurrentStageRequirementsProgress
    {
        get
        {
            if (CurrentStage == null)
                return 0;

            return CurrentStage.RequirementsProgress;
        }
    }

    public void PrepareNextStage()
    {
        FinishStage(currentStageIndex);
        currentStageIndex++;
        InitializeStage(currentStageIndex);
    }

    public void CheckCurrentStageStatus()
    {
        if (selectedScenraio.Stages.Count <= currentStageIndex)
            return;
        
        selectedScenraio.Stages[currentStageIndex].CheckStageStatus();
    }

    public void CheckObjectInRequirements(Object value)
    {
        if (selectedScenraio.Stages.Count <= currentStageIndex)
            return;

        selectedScenraio.Stages[currentStageIndex].CheckObjectInRequirements(value);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeScenario();
    }

    private void InitializeScenario()
    {
        selectedScenraio = Instantiate(selectedScenraio, transform).GetComponent<Scenario>();
        InitializeStage(currentStageIndex);
    }

    private void InitializeStage(int stageIndex)
    {
        if (selectedScenraio.Stages.Count <= stageIndex)
        {
            ScenarioUI.Instance.ShowStage(null);
            NotificationsOverlayUI.Instance.ShowNotification(headerCongrats, contentCongrats, spriteCongrats);
            return;
        }
    
        ScenarioUI.Instance.ShowStage(selectedScenraio.Stages[stageIndex]);
        selectedScenraio.Stages[stageIndex].InitializeStage();
        CurrentStage = selectedScenraio.Stages[stageIndex];
    }

    private void FinishStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= selectedScenraio.Stages.Count)
            return;

        selectedScenraio.Stages[stageIndex].FinishStage();
        CurrentStage = null;
    }
}
