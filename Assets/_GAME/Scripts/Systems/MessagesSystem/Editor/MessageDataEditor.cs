using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(MessageData)), CanEditMultipleObjects]
public class MessageDataEditor : Editor
{
    private MessageData messageData;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(10);
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }

    private void UpdateLists()
    {
        messageData = (MessageData)target;

        List<string> contactList 
            = Resources.LoadAll<MessageContactData>("Messages").Select(x => x.contactID).ToList();

        contactList.Insert(0, "None");
        messageData.contactList = contactList;
    }

    private void OnEnable()
    {
        UpdateLists();
    }
}
