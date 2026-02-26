using UnityEngine;
using UnityEditor;

public class FindMissingScripts
{
    [MenuItem("Tools/Find Missing Scripts")]
    static void Find()
    {
        // Check loaded scenes
        foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            CheckGameObject(go);
        }

        // Check all prefabs in the project
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            foreach (var t in prefab.GetComponentsInChildren<Transform>(true))
            {
                CheckGameObject(t.gameObject, path);
            }
        }

        Debug.Log("Missing script scan complete.");
    }

    static void CheckGameObject(GameObject go, string context = null)
    {
        foreach (var comp in go.GetComponents<Component>())
        {
            if (comp == null)
            {
                string msg = $"Missing script on: {go.name}";
                if (context != null) msg += $" (prefab: {context})";
                else msg += $" (scene: {go.scene.name})";
                Debug.LogWarning(msg, go);
            }
        }
    }
}