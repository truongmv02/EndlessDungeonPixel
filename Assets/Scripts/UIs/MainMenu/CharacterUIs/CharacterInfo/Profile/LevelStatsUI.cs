using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelStatsUI : UIBase
{
    [SerializeField] StatsUI statsUI;
    [SerializeField] Button closeBtn;

    [SerializeField] ToggleGroup toggleGroup;
    [SerializeField] ToggleItem levelPrefab;

    Dictionary<int, List<StatInfo>> stats;
    List<ToggleItem> levels = new List<ToggleItem>();
    CharacterInfo charInfo;


    private void Awake()
    {
        closeBtn.onClick.AddListener(HandleCloseButtonClick);

        for (int i = 1; i <= 5; i++)
        {
            var level = Instantiate(levelPrefab, toggleGroup.transform);
            level.Index = i;
            levels.Add(level);
            level.group = toggleGroup;
            level.GetComponentInChildren<Text>().text = i.ToString();
            level.onValueChanged.AddListener((bool value) =>
            {
                if (value)
                {
                    SetStats(level.Index);
                }
            });

        }
    }

    public void SetInfo(CharacterInfo info)
    {
        charInfo = info;
    }

    public override void Appear()
    {
        base.Appear();
        LoadData();
    }


    void HandleCloseButtonClick()
    {
        Disappear();
    }

    public void LoadData()
    {
        var character = DynamicData.Instance.GetCharacter(charInfo.name);

        stats = DataManager.Instance.CharacterStats.GetAllStats(charInfo.stats);

        int currentLevel = character != null ? character.level : 1;
        levels[currentLevel - 1].isOn = true;
        SetStats(currentLevel);
    }

    void SetStats(int level)
    {
        statsUI.SetInfo(stats[level], stats[stats.Count]);
    }

}
