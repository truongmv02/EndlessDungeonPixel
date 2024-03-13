using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    Dictionary<string, BaseStat> stat_dict = new Dictionary<string, BaseStat>();
    public BaseStat this[string stat]
    {
        get
        {
            if (!stat_dict.ContainsKey(stat))
            {
                return null;
            }
            return stat_dict[stat];
        }
    }


    public void Init(BaseStat[] stats)
    {
        foreach (var stat in stats)
        {
            if (stat_dict.ContainsKey(stat.StatName))
            {
                stat_dict[stat.StatName].Value = stat.Value;
            }
            else
            {
                stat_dict.Add(stat.StatName, stat);
            }
        }
    }

    public void Clear()
    {
        stat_dict.Clear();
    }
}
