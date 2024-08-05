using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{

    [SerializeField] Slider hpSlider;
    [SerializeField] Text healthValue;

    [Space(20)]

    [SerializeField] Slider energySlider;
    [SerializeField] Text energyValue;

    [Space(20)]

    [SerializeField] Slider criticalSlider;
    [SerializeField] Text criticalValue;

    public void SetInfo(List<StatInfo> currentStats, List<StatInfo> lastStats)
    {
        var currentHp = currentStats.Find(x => x.statName == "Health");
        var totalHp = lastStats.Find(x => x.statName == "Health");

        healthValue.text = currentHp.value.ToString("N0");
        hpSlider.value = currentHp.value / totalHp.value;

        var currentEnergy = currentStats.Find(x => x.statName == "Energy");
        var totalEnergy = lastStats.Find(x => x.statName == "Energy");

        energyValue.text = currentEnergy.value.ToString("N0");
        energySlider.value = currentEnergy.value / totalEnergy.value;


    }
}