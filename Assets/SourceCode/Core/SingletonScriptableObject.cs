using System.Linq;
using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();

#if UNITY_EDITOR
                if (!_instance)
                {
                    _instance = CreateInstance<T>();
                    UnityEditor.AssetDatabase.CreateAsset(_instance, "Assets/Configs/Resources/" + typeof(T) + ".asset");
                    UnityEditor.AssetDatabase.SaveAssets();
                    UnityEditor.EditorUtility.FocusProjectWindow();
                }
#endif
            }

            return _instance;
        }
    }
}
