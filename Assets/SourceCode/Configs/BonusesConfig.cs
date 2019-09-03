using System.Collections.Generic;
using UnityEngine;

public interface IBonusesConfig
{
    IEnumerable<BonusConfig> GetBonuses { get; }
}

public class BonusesConfig : SingletonScriptableObject<BonusesConfig>, IBonusesConfig
{
    [SerializeField] private BonusConfig[] _bonuses = default;

    public IEnumerable<BonusConfig> GetBonuses => _bonuses;
}
