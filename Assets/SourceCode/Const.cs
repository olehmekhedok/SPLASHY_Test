public static class Const
{
    public const string Level = "level";
    public const string Platform = "Platform_001";
    public const string LevelCell = "LevelCell";
    public const string BonusCell = "BonusCell";
    public const string QuestCell = "QuestCell";
    public static string AbyssTag = "Abyss";
    public static string PlatformTag = "Platform";

    public static string ToLevelConfigName(int id)
    {
        return Level + "_" + id.ToString("D3");
    }
}