using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Messages/Message Contact Data")]
public class MessageContactData : ScriptableObject
{
    public string contactID => name;
    public string contactName;
    public string contactPhoneNumber;
    [PreviewField] public Sprite contactPhoto;
    public bool hideInContacts;
}
