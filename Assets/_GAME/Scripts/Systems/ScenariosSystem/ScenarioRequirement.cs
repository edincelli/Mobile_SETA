using UnityEngine;

public abstract class ScenarioRequirement : MonoBehaviour
{
    public abstract bool IsDone { get; }
    public abstract string FullfilledRequirementText { get; }

    public abstract void InitializeRequirement();
    public abstract void CheckRequirement(Object value);
}
