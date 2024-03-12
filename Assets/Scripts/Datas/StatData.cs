
using SimpleJSON;
using UnityEngine;

public class StatData : BaseData
{
    public BaseStat[] GetStats(int level)
    {
        var json = GetData(level);
        StatInfo[] stats = JsonUtility.FromJson<StatInfos>(json.ToString()).stats;
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
}
