using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioRequirement_ScenarioUI : ScenarioRequirement
{
    [SerializeField] private RequirementType requirementType;

    private bool isInitialized = false;
    private bool isDone = false;
    public override bool IsDone => isDone;
    public override string FullfilledRequirementText => "";

    public override void InitializeRequirement()
    {
        isInitialized = true;
    }

    public override void CheckRequirement(Object value)
    {

    }

    private void Update()
    {
        if (IsDone)
            return;

        if (isInitialized == false)
            return;

        switch (requirementType)
        {
            case RequirementType.ScrolledDown:
                CheckScrolledDown();
                break;
            default:
                break;
        }

    }

    private void CheckScrolledDown()
    {
        if (ScenarioUI.Instance.ScrolledDown)
            MarkAsDone();
    }

    private void MarkAsDone()
    {
        if (IsDone)
            return;

        isDone = true;
        ScenarioUI.Instance.ShowInfoMessage(FullfilledRequirementText);
        ScenarioManager.Instance.CheckCurrentStageStatus();
    }

    [System.Serializable]
    private enum RequirementType
    {
        ScrolledDown
    }
}
