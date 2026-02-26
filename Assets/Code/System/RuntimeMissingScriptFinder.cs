using UnityEngine;
using UnityEditor;

public class RuntimeMissingScriptFinder : MonoBehaviour
{
    void Start()
    {
        foreach (var go in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            foreach (var comp in go.GetComponents<Component>())
            {
                if (comp == null)
                {
                    Debug.LogError($"FOUND IT! Missing script on: {go.name}", go);
                }
            }
        }
    }
}