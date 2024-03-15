using System.Collections;
using UnityEngine;

public class DetectorData : BaseData
{
    public DetectorData()
    {
        LoadData("Data/Detectors");
    }

    public TargetDetectorInfo GetInfo(string name)
    {
        return GetData<TargetDetectorInfo>(name);
    }
}
