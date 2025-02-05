using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public static class AdjustUI
{
    private const string PrefabSavePath = "Assets/_GAME/Prefabs/App Templates";
    private const string SceneFolderPath = "Assets/Mobile App UI Templates Pro Assets/Scenes";

    [MenuItem("Tools/Adjust Mobile UI", false, -10)]
    private static void AdjustMobileUI(MenuCommand menuCommand)
    {
        if (SceneManager.GetActiveScene().name.Contains("Template") == false)
            return;

        GameObject selectedObject = GameObject.Find("Canvas");

        if (selectedObject == null)
            return;

        CanvasScaler scaller = selectedObject.GetComponent<CanvasScaler>();

        if (scaller == null)
            return;

        scaller.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaller.referenceResolution = new Vector2(1920, 1080);
        scaller.matchWidthOrHeight = 0;

        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;

        RectTransform mobileUI = selectedObject.transform.GetChild(0).GetComponent<RectTransform>();

        GameObject elementsParent = new GameObject(sceneName);
        elementsParent.transform.SetParent(selectedObject.transform, false);
        mobileUI.SetParent(elementsParent.transform, false);

        AspectRatioFitter aspectRatio = elementsParent.AddComponent<AspectRatioFitter>();
        aspectRatio.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        aspectRatio.aspectRatio = 0.5f;
        elementsParent.AddComponent<Image>();
        elementsParent.AddComponent<Mask>();

        elementsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(450, 900);
        mobileUI.sizeDelta = new Vector2(1204, 2408);
        mobileUI.localScale = new Vector3(0.374f, 0.374f, 0.374f);

        ColorUtility.TryParseHtmlString("#022600", out Color bgColor);
        Camera camera;

        try
        {
            GameObject cameraObject = GameObject.Find("Camera");

            if (cameraObject == null)
                cameraObject = GameObject.Find("Main Camera");

            camera = cameraObject.GetComponent<Camera>();
        }
        catch {
            Debug.LogError($"Failed to find camera object.");
            return;
        }


        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = bgColor;

        //save prefab
        if (SaveAsPrefab(elementsParent) == false)
            return;


        //save scene
        EditorSceneManager.MarkSceneDirty(scene);

        if (EditorSceneManager.SaveScene(scene))
        {
            Debug.Log($"Scene saved successfully: {scene.name}");
        }
        else
        {
            Debug.LogError($"Failed to save the scene: {scene.name}");
            return;
        }

        //load next scene
        LoadNextScene(scene);

    }

    [MenuItem("Tools/Adjust Mobile UI", true)]
    private static bool ValidateAdjustMobileUI()
    {
        if (SceneManager.GetActiveScene().name.Contains("Template") == false)
            return false;

        GameObject selectedObject = GameObject.Find("Canvas");

        if (selectedObject == null)
            return false;

        CanvasScaler scaller = selectedObject.GetComponent<CanvasScaler>();

        if(scaller == null)
            return false;

        return true;
    }

    private static bool SaveAsPrefab(GameObject objectToSave)
    {
        if (AssetDatabase.IsValidFolder(PrefabSavePath) == false)
        {
            Debug.LogError($"Target folder does not exist: {PrefabSavePath}");
            return false;
        }

        string prefabPath = $"{PrefabSavePath}/{objectToSave.name}.prefab";

        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(objectToSave, prefabPath);

        if (prefab != null)
        {
            Debug.Log($"Prefab saved successfully at: {prefabPath}");

            GameObject prefabInstance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, objectToSave.transform.parent);

            if (prefabInstance != null)
            {
                prefabInstance.transform.position = objectToSave.transform.position;
                prefabInstance.transform.rotation = objectToSave.transform.rotation;
                prefabInstance.transform.localScale = objectToSave.transform.localScale;

                Object.DestroyImmediate(objectToSave);

                Debug.Log($"Replaced object in hierarchy with prefab instance: {prefabInstance.name}");
            }

            return true;
        }
        else
        {
            Debug.LogError("Failed to save Prefab!");
            return false;
        }

    }

    private static void LoadNextScene(Scene activeScene) 
    {
        // Pobierz wszystkie sceny w folderze
        string[] scenePaths = AssetDatabase.FindAssets("t:Scene", new[] { SceneFolderPath });

        // Posortuj sceny alfabetycznie
        System.Array.Sort(scenePaths, (a, b) =>
            AssetDatabase.GUIDToAssetPath(a).CompareTo(AssetDatabase.GUIDToAssetPath(b)));

        // Znajdü indeks aktualnej sceny
        string activeScenePath = activeScene.path;
        int currentSceneIndex = -1;

        for (int i = 0; i < scenePaths.Length; i++)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(scenePaths[i]);
            if (scenePath == activeScenePath)
            {
                currentSceneIndex = i;
                break;
            }
        }

        // Jeúli jest kolejna scena, za≥aduj jπ
        if (currentSceneIndex >= 0 && currentSceneIndex < scenePaths.Length - 1)
        {
            string nextScenePath = AssetDatabase.GUIDToAssetPath(scenePaths[currentSceneIndex + 1]);
            Debug.Log($"Loading next scene: {nextScenePath}");
            EditorSceneManager.OpenScene(nextScenePath);
        }
        else
        {
            Debug.LogWarning("No next scene found. This is the last scene in the folder.");
        }
    }
}
