using UnityEngine;

public static class PrefabUtilityHelper
{
    public static bool IsPrefabInEditMode(GameObject gameObject)
    {

#if UNITY_EDITOR

        var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();

        if (prefabStage != null && prefabStage.prefabContentsRoot == gameObject)
            return true;

#endif

        return false;
    }
}
