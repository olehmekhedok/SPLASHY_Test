using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformConfig
{
    public PlatformType Type;
    public Vector3 Position;
}

[Serializable]
public class LevelConfig
{
    public int Id;
    public List<PlatformType> Path;
    public List<PlatformConfig> Platforms;
}
