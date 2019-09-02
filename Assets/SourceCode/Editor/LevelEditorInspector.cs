using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorInspector : Editor
{
    private IGameConfig _config;
    public IGameConfig Config => _config ?? (_config = StaticContext.Container.Resolve<IGameConfig>());
    private LevelEditor Target => (LevelEditor) target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Update Platforms"))
        {
            var gap = Config.PlatformGap;
            var platforms = FindObjectsOfType<Platform>();

            foreach (var child in platforms)
            {
                var currentPosition = child.transform.position;
                currentPosition.y = 0;
                currentPosition.z = (float) (Math.Round(currentPosition.z / gap) * gap);
                child.transform.position = currentPosition;
            }

            EditorSceneManager.MarkAllScenesDirty();
            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Update Level Path"))
        {
            Target.LevelPath = Selection.objects
                .Cast<GameObject>()
                .Select(o => o.GetComponent<Platform>())
                .Where(o => o != null)
                .OrderBy(p => p.transform.position.z)
                .ToArray();

            EditorSceneManager.MarkAllScenesDirty();
            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Select Level Path"))
        {
            Selection.objects = Target.LevelPath.Select(p => p.gameObject).Cast<Object>().ToArray();
        }

        if (GUILayout.Button("Randomize Platforms Types"))
        {
            var platforms = FindObjectsOfType<Platform>();
            var allTypes = Enum.GetValues(typeof(PlatformType)).Cast<PlatformType>().Where(t => t != PlatformType.None).ToArray();

            foreach (Platform platform in platforms)
            {
                var index = Random.Range(0, allTypes.Length);
                var sr = new SerializedObject(platform);
                sr.FindProperty("_type").enumValueIndex = (int) allTypes[index];
                sr.ApplyModifiedProperties();
            }
            EditorSceneManager.MarkAllScenesDirty();
            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Save Config"))
        {
            var sceneName = SceneManager.GetActiveScene().name;
            var id = sceneName.Split('_').Last();
            var levelConfig = new LevelConfig
            {
                Platforms = FindObjectsOfType<Platform>()
                    .Select(p => new PlatformConfig {Type = p.Type, Position = p.transform.position}).ToList(),

                Path = Target.LevelPath.Select(p => p.Type).ToList(),
                Id = int.Parse(id)
            };

            var jsonString = JsonUtility.ToJson(levelConfig);
            var filePath = Application.dataPath + "/Resources/" + sceneName + ".json";
            File.WriteAllText(filePath, jsonString);
        }
    }

    private void OnSceneGUI()
    {
        if (Target.LevelPath == null)
            return;

        var previous = Vector3.zero;
        foreach (var platform in Target.LevelPath)
        {
            Handles.DrawLine(previous, platform.transform.position);
            previous = platform.transform.position;
        }
    }
}
