using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    Dictionary<string, BaseStat> stat_dict = new Dictionary<string, BaseStat>();
    public BaseStat this[string stat] => stat_dict[stat];

    public void Init(BaseStat[] stats)
    {
        foreach (var stat in stats)
        {
            stat_dict.Add(stat.StatName, stat);
        }
    }

    public void Clear()
    {
        stat_dict.Clear();
    }
}
