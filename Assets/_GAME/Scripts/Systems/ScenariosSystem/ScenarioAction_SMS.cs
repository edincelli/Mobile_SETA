using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioAction_SMS : ScenarioAction
{
    [SerializeField] private List<MessageData> messagesToSend = new List<MessageData>();

    public void SendSMSs()
    {
        for (int i = 0; i < messagesToSend.Count; i++)
        {
            MessagesManager.Instance.RecieveMessage(messagesToSend[i], true);
        }
    }
}
