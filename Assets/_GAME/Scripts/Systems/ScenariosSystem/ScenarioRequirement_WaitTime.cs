using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioRequirement_WaitTime : ScenarioRequirement
{
    [SerializeField] private float waitSeconds = 5;

    private float initialWaitTime;
    private bool isInitialized = false;
    private bool update = true;

    public override bool IsDone => waitSeconds < 0;
    public override string FullfilledRequirementText => "";
    public float Progress => 1 - Mathf.Clamp01(waitSeconds / initialWaitTime);

    public override void InitializeRequirement()
    {
        isInitialized = true;
        initialWaitTime = waitSeconds;
    }

    public override void CheckRequirement(Object value)
    {

    }

    private void Update()
    {
        if (update == false)
            return;

        if (isInitialized == false)
            return;

        waitSeconds -= Time.deltaTime;

        if(waitSeconds < 0)
        {
            ScenarioManager.Instance.CheckCurrentStageStatus();
            update = false;
        }
    }
}
