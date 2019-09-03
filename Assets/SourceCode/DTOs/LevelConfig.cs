using System;
using System.Collections.Generic;

[Serializable]
public class LevelConfig
{
    public int Id;
    public List<PlatformType> Path;
    public List<PlatformConfig> Platforms;
}
