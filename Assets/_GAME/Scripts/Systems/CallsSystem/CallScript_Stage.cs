using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class CallScript_Stage
{
    [SerializeField, TextArea(5, 10)] private string info;
    [TextArea(10,20)] public string transcript;
    public AudioClip audioClip;
    [Range(0, 30)] public float stageWaitTime = 5;
}
