using UnityEditor;

public class GameConfigSelector
{
    [MenuItem("Tools/GameConfig")]
    public static void SelectGameConfig(MenuCommand menuCommand)
    {
        Selection.activeObject = GameConfig.Instance;
    }
}
