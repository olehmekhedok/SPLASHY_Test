using System.Collections.Generic;
using UnityEngine;

public interface IQuestsConfig
{
    IEnumerable<QuestsSetConfig> GetQuests { get; }
}

public class QuestsConfig : SingletonScriptableObject<QuestsConfig>, IQuestsConfig
{
    [SerializeField] private QuestsSetConfig[] _quests = default;

    public IEnumerable<QuestsSetConfig> GetQuests => _quests;
}
