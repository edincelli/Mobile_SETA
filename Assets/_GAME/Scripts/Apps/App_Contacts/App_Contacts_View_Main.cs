using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class App_Contacts_View_Main : AppView
{
    [SerializeField] private App_Contacts appContacts;
    [SerializeField] private Transform contactsPanelParent;
    [SerializeField] private GameObject contactPanelPrefab;

    private List<MessageContactData> contacts = new List<MessageContactData>();

    public override void ShowView()
    {
        UpdatePanels();
        base.ShowView();
    }

    private void UpdatePanels()
    {
        contacts.Clear();
        contacts.AddRange(MessagesManager.Contacts.Where(c => c.hideInContacts == false));

        int currentButtonsCount = contactsPanelParent.childCount;

        for (int i = 0; i < contacts.Count; i++)
        {
            int tempI = i;
            App_Contacts_ChatPanel button;

            if (i >= currentButtonsCount)
            {
                Instantiate(contactPanelPrefab, contactsPanelParent);
            }

            button = contactsPanelParent.GetChild(i).GetComponent<App_Contacts_ChatPanel>();
            button.SetupPanel(contacts[tempI], appContacts);
        }

        for (int i = currentButtonsCount; i > contacts.Count; i--)
        {
            contactsPanelParent.GetChild(i - 1).gameObject.Destroy();
        }
    }
}
