using System.Collections.Generic;
using UnityEngine;

public interface IMissionsConfig
{
    IEnumerable<MissionsSetConfig> GetMissions { get; }
}

public class MissionsConfig : SingletonScriptableObject<MissionsConfig>, IMissionsConfig
{
    [SerializeField] private MissionsSetConfig[] _missions = default;

    public IEnumerable<MissionsSetConfig> GetMissions => _missions;
}
