using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class ScenarioElement
{
    [SerializeField] private ElementType elementType;

    [SerializeField, TextArea(10, 20), ShowIf("elementType", ElementType.Text)] 
    private string text;

    [SerializeField, Range(50, 500), ShowIf("elementType", ElementType.Picture)] 
    private float height = 150;
    [SerializeField, PreviewField, ShowIf("elementType", ElementType.Picture)] 
    private Sprite sprite;

    public ElementType TypeElement => elementType;
    public string Text => text;
    public float Height => height;
    public Sprite Sprite => sprite;

    public enum ElementType
    {
        Text,
        Picture
    }
}
