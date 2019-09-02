using System;
using System.Collections.Generic;
using UnityEngine;

public interface IMissionsConfig
{
    IEnumerable<MissionsSetConfig> GetMissions { get; }
}

[Serializable]
public struct MissionConfig
{
    public MissionType Type;
    public int Amount;
}

[Serializable]
public struct MissionsSetConfig
{
    public MissionConfig[] Missions;
}

public class MissionsConfig : SingletonScriptableObject<MissionsConfig>, IMissionsConfig
{
    [SerializeField] private MissionsSetConfig[] _missions = default;

    public IEnumerable<MissionsSetConfig> GetMissions => _missions;
}
