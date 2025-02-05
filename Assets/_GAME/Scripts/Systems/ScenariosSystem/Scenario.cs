using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public class Scenario : MonoBehaviour
{
    [SerializeField] private string scenarioName;
    [SerializeField, TextArea(3, 10)] private string scenarioDescription;

    public string ScenarioName { get => scenarioName; }
    public string ScenarioDescription { get => scenarioDescription; }

    public List<ScenarioStage> Stages
    {
        get
        {
            List<ScenarioStage> stages = new List<ScenarioStage>();
            stages.AddRange(GetComponentsInChildren<ScenarioStage>());
            return stages;
        }
    }

    [Button("Add Stage"), ShowIf("@this.IsPrefabInEditMode()")]
    private void AddStage()
    {
        int stageIndex = transform.childCount + 1;
        GameObject newStageObject = new GameObject($"Stage {stageIndex}");
        newStageObject.AddComponent<ScenarioStage>();
        newStageObject.transform.SetParent(transform);
    }

    [Button("Rename Stages"), ShowIf("@this.IsPrefabInEditMode()")]
    private void RenameStages()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).name = $"Stage {i + 1}"; ;
        }
    }

#if UNITY_EDITOR
    [Button("Open Asset"), HideIf("@this.IsPrefabInEditMode()")]
    private void OpenAsset()
    {
        EditorApplication.delayCall += () =>
        {
            EditorGUIUtility.PingObject(gameObject);
            AssetDatabase.OpenAsset(gameObject, 0);
        };
    }

    [MenuItem("Assets/Create/Scenario", priority = -1)]
    private static void CreateNewScenario()
    {
        GameObject newScenarioObject = new GameObject("NewScenario");
        Scenario scenario = newScenarioObject.AddComponent<Scenario>();

        string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (!string.IsNullOrEmpty(folderPath) && !AssetDatabase.IsValidFolder(folderPath))
            folderPath = System.IO.Path.GetDirectoryName(folderPath);

        if (string.IsNullOrEmpty(folderPath))
            folderPath = "Assets";

        string path = AssetDatabase.GenerateUniqueAssetPath(System.IO.Path.Combine(folderPath, "NewScenario.prefab"));

        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(newScenarioObject, path);
        Selection.activeObject = prefab;
        DestroyImmediate(newScenarioObject);
    }

    private bool IsPrefabInEditMode()
    {
        return PrefabUtilityHelper.IsPrefabInEditMode(gameObject);
    }
#endif
}
