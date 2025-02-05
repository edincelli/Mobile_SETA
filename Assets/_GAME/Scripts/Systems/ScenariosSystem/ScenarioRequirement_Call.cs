using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScenarioRequirement_Call : ScenarioRequirement
{
    [SerializeField] private string fullfilledRequirementText = "";

    [Space]
    [SerializeField] private RequirementType requirementType;
    [ShowIf("requirementType", RequirementType.CallStage)]
    [SerializeField] private int requiredStage;

    private bool isInitialized = false;
    private bool isDone = false;

    public override bool IsDone => isDone;
    public override string FullfilledRequirementText => fullfilledRequirementText;

    public override void InitializeRequirement()
    {
        isInitialized = true;
    }

    public override void CheckRequirement(Object value)
    {
        if (isDone)
            return;

        switch (requirementType)
        {
            case RequirementType.CallStarted:
                CheckCallStarted();
                break;
            case RequirementType.CallStage:
                CheckCallStage();
                break;
            case RequirementType.CallEnded:
                CheckCallEnded();
                break;
            default:
                break;
        }
    }

    private void CheckCallStarted()
    {
        if (CallsManager.Instance.CallInProgress == false)
            return;

        MarkAsDone();
    }

    private void CheckCallStage()
    {
        int stageCount = CallsManager.Instance.CurrentCallScript.stages.Count;

        if (stageCount > 0)
            requiredStage = Mathf.Clamp(requiredStage, 0, stageCount - 1);
        else
            requiredStage = 0;

        if (CallsManager.Instance.CallStage < requiredStage)
            return;

        MarkAsDone();
    }

    private void CheckCallEnded()
    {
        if (CallsManager.Instance.CallIncoming)
            return;

        if (CallsManager.Instance.CallInProgress)
            return;

        MarkAsDone();
    }

    private void MarkAsDone()
    {
        if(IsDone) 
            return;

        isDone = true;
        ScenarioUI.Instance.ShowInfoMessage(FullfilledRequirementText);
        ScenarioManager.Instance.CheckCurrentStageStatus();
    }

    private void FixedUpdate()
    {
        if (isInitialized == false)
            return;

        if (IsDone)
            return;

        CheckRequirement(null);
    }

    [System.Serializable]
    public enum RequirementType
    {
        CallStarted,
        CallStage,
        CallEnded
    }
}
