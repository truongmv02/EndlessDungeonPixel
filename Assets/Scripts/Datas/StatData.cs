
using SimpleJSON;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatData : StaticData
{
    public BaseStat[] GetStats(int level)
    {
        var json = GetData(level);
        StatInfo[] stats = GetData<StatInfos>(level).stats;
        return CreateBaseStats(stats);


    }
    private BaseStat[] CreateBaseStats(StatInfo[] statInfos)
    {

        BaseStat[] stats = new BaseStat[statInfos.Length];
        for (int i = 0; i < statInfos.Length; i++)
        {
            stats[i] = new BaseStat() { StatName = statInfos[i].statName, Value = statInfos[i].value };
        }
        return stats;
    }

    public Dictionary<int, List<StatInfo>> GetAllStatInfo()
    {
        Dictionary<int, List<StatInfo>> result = new Dictionary<int, List<StatInfo>>();

        int i = 1;
        foreach (var data in data_dict)
        {
            StatInfo[] stats = GetData<StatInfos>(data.Value.AsObject).stats;
            result.Add(i++, stats.ToList());
        }
        return result;
    }
}
