#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class MissingScriptFinder : MonoBehaviour
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    public static void FindMissingScriptsInScene()
    {
        int count = 0;
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objects)
        {
            Component[] components = obj.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing script on GameObject: {obj.name}", obj);
                    count++;
                }
            }
        }

        Debug.Log($"Done! Found {count} missing script(s) in the scene.");
    }
}
#endif