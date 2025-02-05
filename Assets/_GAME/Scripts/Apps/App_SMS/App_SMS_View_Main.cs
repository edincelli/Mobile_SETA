using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App_SMS_View_Main : AppView
{
    [SerializeField] private App_SMS appSMS;
    [SerializeField] private Transform smsPanelParent;
    [SerializeField] private GameObject smsPanelPrefab;

    private List<MessageInfoStack> messageStacks => PlayerController.PlayerInfo.messageStacks;

    public override void ShowView()
    {
        UpdatePanels();
        base.ShowView();
    }

    private void UpdatePanels()
    {
        int currentButtonsCount = smsPanelParent.childCount;

        for (int i = 0; i < messageStacks.Count; i++)
        {
            int tempI = i;
            App_SMS_ChatPanel button;

            if (i >= currentButtonsCount)
            {
                Instantiate(smsPanelPrefab, smsPanelParent);
            }

            button = smsPanelParent.GetChild(i).GetComponent<App_SMS_ChatPanel>();
            button.SetupPanel(messageStacks[tempI], appSMS);
        }

        for (int i = currentButtonsCount; i > messageStacks.Count; i--)
        {
            smsPanelParent.GetChild(i - 1).gameObject.Destroy();
        }
    }
}
