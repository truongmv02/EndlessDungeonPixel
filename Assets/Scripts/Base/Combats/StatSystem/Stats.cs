using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Stats : MonoBehaviour
{
    Dictionary<string, BaseStat> stat_dict = new();

    public static void IncrementCurrentStat(string statName, float value, Stats stats)
    {
        var statNameMax = statName.Replace("Current", "");
        var statMax = stats[statNameMax];

        float maxValue = Mathf.Infinity;
        if (statMax != null)
        {
            maxValue = statMax.Value;
        }

        var statBuff = stats[statName];
        statBuff.Value = Mathf.Clamp(statBuff.Value + value, 0, maxValue);
    }

    public Dictionary<string, BaseStat> GetStats()
    {
        return stat_dict;
    }

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
        set
        {
            if (stat_dict.ContainsKey(value.StatName))
            {
                stat_dict[value.StatName].Value = value.Value;
            }
            else
            {
                stat_dict.Add(value.StatName, value);
            }
        }
    }

    public void AddStat(Stats stats)
    {
        Dictionary<string, BaseStat> statDict = stats.GetStats();

        foreach (KeyValuePair<string, BaseStat> pair in statDict)
        {
            this[pair.Key] = pair.Value;
        }

    }
    public void Init(BaseStat[] stats)
    {
        foreach (var stat in stats)
        {

            this[stat.StatName] = stat;
            /*if (stat_dict.ContainsKey(stat.StatName))
            {
                stat_dict[stat.StatName].Value = stat.Value;
            }
            else
            {
                stat_dict.Add(stat.StatName, stat);
            }*/
        }
    }

    public void Clear()
    {
        stat_dict.Clear();
    }
}
