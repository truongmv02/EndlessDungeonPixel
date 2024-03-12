

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
}

