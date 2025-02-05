using UnityEditor;
using UnityEngine;

public class GameSystemComponent : MonoBehaviour
{
    private void Reset()
    {

        //#if UNITY_EDITOR
        //        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(gameObject), this.GetType().Name);
        //#else
        gameObject.name = this.GetType().Name;
        //#endif

        transform.position = Vector3.zero;
    }
}
