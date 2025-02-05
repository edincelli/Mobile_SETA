using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScenarioRequirement_SMS : ScenarioRequirement
{
    [SerializeField] private string fullfilledRequirementText = "You have correctly indicated the message. Good job! You can continue!";
    [SerializeField] private string wrongMessageText = "You clicked wrong message. Try one more time.";

    [SerializeField] private RequirementType requirementType;
    [SerializeField, ShowIf("requirementType", RequirementType.ClickSMS)] 
    private List<MessageData> messagesToClick = new List<MessageData>();
    [SerializeField, ShowIf("requirementType", RequirementType.OpenChat)]
    private MessageContactData contactToOpenChat;

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

        if (requirementType == RequirementType.ClickSMS)
        {
            if (value.GetType() != typeof(MessageData))
                return;

            if (messagesToClick.Contains(value))
                SetAsDone();
            else
                ScenarioUI.Instance.ShowErrorMessage(wrongMessageText);

            return;
        }
        else if (requirementType == RequirementType.OpenChat)
        {
            if (value.GetType() != typeof(MessageContactData))
                return;

            if (contactToOpenChat == (MessageContactData)value)
                SetAsDone();
            else
                ScenarioUI.Instance.ShowErrorMessage(wrongMessageText);

            return;
        }

        ScenarioUI.Instance.ShowErrorMessage(wrongMessageText);
    }

    private void SetAsDone()
    {
        isDone = true;
        ScenarioUI.Instance.ShowInfoMessage(FullfilledRequirementText);
        ScenarioManager.Instance.CheckCurrentStageStatus();
    }

    [System.Serializable]
    public enum RequirementType
    {
        ClickSMS,
        OpenChat
    }
}
