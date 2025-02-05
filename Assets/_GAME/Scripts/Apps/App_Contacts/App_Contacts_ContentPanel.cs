using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class App_Contacts_ChatPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI contentLabel;
    [SerializeField] private Image contactImage;

    private App_Contacts appContacts;
    private MessageContactData contactData;

    public void SetupPanel(MessageContactData contactData, App_Contacts appContacts)
    {
        this.contactData = contactData;
        this.appContacts = appContacts;

        nameLabel.text = contactData.contactName;
        contentLabel.text = contactData.contactPhoneNumber;

        Sprite contactPicture = contactData.contactPhoto;

        if (contactImage != null)
            contactImage.sprite = contactPicture;
    }

    public void Click()
    {
        Debug.Log($"Contact clicked - {contactData.contactID}");
    }
}
