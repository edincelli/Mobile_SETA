using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Core Elements Preset")]
public class CoreElementsPreset : ScriptableObject
{
    public List<GameObject> coreElements = new List<GameObject>();
}
