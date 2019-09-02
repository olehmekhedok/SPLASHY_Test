using System;
using System.Collections.Generic;
using UnityEngine;
public interface IBonusesConfig
{
    IEnumerable<BonusConfig> GetBonuses { get; }
}

[Serializable]
public struct BonusConfig
{
    public int Reward;
    public int Hours;
}

public class BonusesConfig : SingletonScriptableObject<BonusesConfig>, IBonusesConfig
{
    [SerializeField] private BonusConfig[] _bonuses = default;

    public IEnumerable<BonusConfig> GetBonuses => _bonuses;
}
