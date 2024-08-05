using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : UIBase
{
    [SerializeField] Text characterName;
    [SerializeField] Image characterImage;

    [SerializeField] Transform currentLevelTransform;

    [Space(10)]
    [Header("Stats")]
    [SerializeField] StatsUI statsUI;
    [SerializeField] LevelStatsUI levelStatsUI;

    [Space(10)]
    [Header("Weapon")]
    [SerializeField] Image weaponImage;

    [Space(10)]
    [Header("Buttons")]
    [SerializeField] Button levelInfoBtn;
    [SerializeField] Button upgradeBtn;
    [SerializeField] Button closeBtn;

    CharacterInfo charInfo;

    private void Awake()
    {
        closeBtn.onClick.AddListener(HandleCloseButtonClick);
        levelInfoBtn.onClick.AddListener(HandleLevelInfoButtonClick);
        upgradeBtn.onClick.AddListener(HandleUpgradeButtonClick);
    }

    public void SetInfo(CharacterInfo info)
    {
        charInfo = info;
        characterName.text = info.name;
        SetCharacterSprite(info.sprite);
        SetWeaponSprite(info.initialWeapon.sprite);
        SetData();
    }

    void SetData()
    {
        var character = DynamicData.Instance.GetCharacter(charInfo.name);
        SetCharacterLevel(character);
        SetCharacterStat(character);
        SetUpgradeButtonInfo(character);
    }
    public void SetCharacterSprite(Sprite sprite)
    {
        characterImage.sprite = sprite;
        characterImage.SetNativeSize();
    }

    public void SetWeaponSprite(Sprite sprite)
    {
        weaponImage.sprite = sprite;
        weaponImage.SetNativeSize();
    }

    public void SetCharacterLevel(PurchasedCharacterInfo character)
    {
        int level = character != null ? character.level : 0;
        for (int i = 0; i < 5; i++)
        {
            Image star = currentLevelTransform.GetChild(i).GetComponent<Image>();
            if (i < level)
                star.enabled = true;
            else
                star.enabled = false;
        }
    }

    public void SetCharacterStat(PurchasedCharacterInfo character)
    {
        int level = character != null ? character.level : 1;
        Dictionary<int, List<StatInfo>> statDict = DataManager.Instance.CharacterStats.GetAllStats(charInfo.stats);
        var currentStats = statDict[level];
        var lastStats = statDict[statDict.Count];

        statsUI.SetInfo(currentStats, lastStats);
    }

    public void SetUpgradeButtonInfo(PurchasedCharacterInfo character)
    {
        int level = character != null ? character.level : 0;

        if (level == charInfo.prices.Length)
        {
            return;
        }
        else if (level == 0)
        {
            upgradeBtn.transform.Find("Title").GetComponent<Text>().text = "Un lock";
        }
        else if (level == 1)
        {
            upgradeBtn.transform.Find("Title").GetComponent<Text>().text = "Upgrade";
        }

        var price = charInfo.prices[level];
        upgradeBtn.interactable = DynamicData.Instance.Data.coin >= price;
        upgradeBtn.transform.Find("PriceHolder").Find("Value").GetComponent<Text>().text = price.ToString();

    }

    private void HandleCloseButtonClick()
    {
        Disappear();
        MainMenuUIManager.Instance.characterSelectionUI.SetButton();
    }

    void HandleLevelInfoButtonClick()
    {
        levelStatsUI.SetInfo(charInfo);
        levelStatsUI.Appear();
    }

    void HandleUpgradeButtonClick()
    {
        bool result;
        var character = DynamicData.Instance.GetCharacter(charInfo.name);

        if (character == null)
            result = DynamicData.Instance.AddPurchasedCharacter(charInfo.name);
        else
            result = DynamicData.Instance.UpgradeCharacter(charInfo.name);

        if (result)
        {
            MainMenuUIManager.Instance.SetCoinText();
            SetData();
        }
    }

}
