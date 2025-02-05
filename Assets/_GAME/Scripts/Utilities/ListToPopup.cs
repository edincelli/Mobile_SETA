using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ListToPopupAttribute : PropertyAttribute
{
    public Type type;
    public string propertyName;
    public string label;

    public ListToPopupAttribute(Type type, string propertyName, string label)
    {
        this.type = type;
        this.propertyName = propertyName;
        this.label = label;
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ListToPopupAttribute))]
public class ListToPopupDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ListToPopupAttribute attrib = attribute as ListToPopupAttribute;
        List<string> stringList = null;

        System.Reflection.FieldInfo field = attrib.type.GetField(attrib.propertyName);

        if (field != null)
            stringList = field.GetValue(attrib.type) as List<string>;

        if (stringList.IsNullOrEmpty())
        {
            property.stringValue = "-";
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        int selectedIndex = Mathf.Max(stringList.IndexOf(property.stringValue), 0);

        if (string.IsNullOrEmpty(attrib.label))
            selectedIndex = EditorGUI.Popup(position, selectedIndex, stringList.ToArray());
        else
            selectedIndex = EditorGUI.Popup(position, attrib.label, selectedIndex, stringList.ToArray());

        property.stringValue = stringList[selectedIndex];
    }
}

#endif
