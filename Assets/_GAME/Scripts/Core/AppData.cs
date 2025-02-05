using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "App Data")]
public class AppData : ScriptableObject
{
    public string ID => name;
    public string appName;
    [PreviewField] public Sprite appIcon;

    public GameObject appPrefab;

    [Space]
    public bool hiddenApp;
}
