

using System.Collections.Generic;

public class StatDatas : BaseMultiData
{
    public StatDatas(string path)
    {
        LoadData<StatData>("Data/Stats/" + path);
    }

    public BaseStat[] GetStats(string name, int level = 1)
    {
        var statData = Datas[name] as StatData;
        return statData.GetStats(level);
    }

    public Dictionary<int, List<StatInfo>> GetAllStats(string name)
    {
        var statData = Datas[name] as StatData;
        return statData.GetAllStatInfo();
    }

}

