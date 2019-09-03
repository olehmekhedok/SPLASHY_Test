using System;

[Serializable]
public struct QuestsSetConfig
{
    public int Reward;
    public QuestConfig[] Quests;
}

[Serializable]
public struct QuestConfig
{
    public string Title;
    public QuestType Type;
    public int Amount;
}