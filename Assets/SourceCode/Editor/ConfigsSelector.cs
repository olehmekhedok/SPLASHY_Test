using UnityEditor;

public class ConfigsSelector
{
    [MenuItem("Tools/GameConfig")]
    public static void SelectGameConfig(MenuCommand menuCommand)
    {
        Selection.activeObject = GameConfig.Instance;
    }

    [MenuItem("Tools/BonusesConfig")]
    public static void SelectBonusesConfig(MenuCommand menuCommand)
    {
        Selection.activeObject = BonusesConfig.Instance;
    }
    
    [MenuItem("Tools/MissionsConfig")]
    public static void SelectMissionsConfig(MenuCommand menuCommand)
    {
        Selection.activeObject = MissionsConfig.Instance;
    }
}
