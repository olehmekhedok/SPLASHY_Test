using System;

[Serializable]
public struct MissionsSetConfig
{
    public int Reward;
    public MissionConfig[] Missions;
}

[Serializable]
public struct MissionConfig
{
    public string Title;
    public MissionType Type;
    public int Amount;
}