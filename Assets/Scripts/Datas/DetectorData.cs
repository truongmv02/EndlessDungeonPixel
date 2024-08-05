using System.Collections;
using UnityEngine;

public class DetectorData : StaticData
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
