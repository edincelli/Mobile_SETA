using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AppIcon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AppData appData;

    [Header("UI Elements")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI labelTMP;

    [Button("Update UI"), PropertySpace(10)]
    private void UpdateUI_Editor()
    {
        UpdateUI();

#if UNITY_EDITOR
        gameObject.name = $"AppIcon - {appData.appName}";
        EditorUtility.SetDirty(iconImage);
        EditorUtility.SetDirty(labelTMP);
#endif
    }

    [Button("Duplicate Object"), PropertySpace(20)]
    private void DuplicateObject()
    {
#if UNITY_EDITOR
        GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject);
        GameObject newObject = PrefabUtility.InstantiatePrefab(prefab, Selection.activeGameObject.transform.parent) as GameObject;
        Selection.activeGameObject = newObject;
#endif
    }

    public void ClickAppIcon()
    {
        PhoneSimulatorManager.Instance.OpenApp(appData.ID);
    }

    private void UpdateUI()
    {
        iconImage.sprite = appData.appIcon;
        labelTMP.text = appData.appName;
    }

    private void Start()
    {
        UpdateUI();
    }
}
