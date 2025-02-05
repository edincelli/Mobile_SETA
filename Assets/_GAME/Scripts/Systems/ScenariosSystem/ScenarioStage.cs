using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioStage : MonoBehaviour
{
    [SerializeField] private List<ScenarioElement> scenarioElements = new List<ScenarioElement>();
    [Space]
    [SerializeField, Range(0, 10)] private float actionsDelay = 0;
    [SerializeField] private UnityEvent scenarioActionsStart;
    [SerializeField] private UnityEvent scenarioActionsDelayed;
    [SerializeField] private UnityEvent scenarioActionsEnd;

    [Space]
    [SerializeField] private bool autoContinue;
    [SerializeField] private string buttonText = "Continue";
    
    [Space]
    [SerializeField] private bool showChildElements = false;

    [SerializeField, FoldoutGroup("Child Elements"), ShowIf("showChildElements")] private Transform actionsParent;
    [SerializeField, FoldoutGroup("Child Elements"), ShowIf("showChildElements")] private Transform requirementsParent;

    private List<ScenarioRequirement> requirements = new List<ScenarioRequirement>();

    public List<ScenarioElement> ScenarioElements => scenarioElements;
    public string ButtonText { get => buttonText; }
    public float RequirementsProgress
    {
        get
        {
            if (requirements.IsNullOrEmpty())
                return 0;

            if (requirements.Count > 1)
                return 0;

            if (requirements[0].GetType() == typeof(ScenarioRequirement_WaitTime))
                return ((ScenarioRequirement_WaitTime)requirements[0]).Progress;

            return 0;
        }
    }


    public void InitializeStage()
    {
        StartCoroutine(WaitAndInvoke());

        if(autoContinue && requirements.Count == 0)
            autoContinue = false;

        scenarioActionsStart.Invoke();
    }

    public void FinishStage()
    {
        scenarioActionsEnd.Invoke();
    }

    public void CheckObjectInRequirements(Object value)
    {
        for (int i = 0; i < requirements.Count; i++)
        {
            requirements[i].CheckRequirement(value);
        }
    }

    public void CheckStageStatus()
    {
        bool isAllDone = StageStatus();
        ScenarioUI.Instance.UpdateButton(isAllDone);

        if (autoContinue && isAllDone)
            ScenarioUI.Instance.ClickScenarioButton();
    }

    public bool StageStatus()
    {
        for (int i = 0; i < requirements.Count; i++)
        {
            if (requirements[i].IsDone == false)
                return false;
        }

        return true;
    }

    private void Awake()
    {
        ScenarioRequirement[] tempRquirements = requirementsParent.GetComponents<ScenarioRequirement>();

        if (tempRquirements.Length > 0)
            requirements.AddRange(tempRquirements);
    }

    private void Reset()
    {
        GameObject actionsObject = new GameObject("Actions");
        GameObject requirementsObject = new GameObject("Requirements");

        actionsObject.transform.SetParent(transform);
        requirementsObject.transform.SetParent(transform);

        actionsParent = actionsObject.transform;
        requirementsParent = requirementsObject.transform;
    }

    private IEnumerator WaitAndInvoke()
    {
        if (actionsDelay <= 0)
            actionsDelay = float.Epsilon;

        yield return new WaitForSeconds(actionsDelay);
        scenarioActionsDelayed.Invoke();

        for (int i = 0; i < requirements.Count; i++)
        {
            requirements[i].InitializeRequirement();
        }

        CheckStageStatus();
    }

#if UNITY_EDITOR
    [Button("Show Requirements")]
    private void ShowRequirements()
    {
        Selection.activeGameObject = requirementsParent.gameObject;
    }

    [Button("Show Actions")]
    private void ShowActions()
    {
        Selection.activeGameObject = actionsParent.gameObject;
    }
#endif
}
