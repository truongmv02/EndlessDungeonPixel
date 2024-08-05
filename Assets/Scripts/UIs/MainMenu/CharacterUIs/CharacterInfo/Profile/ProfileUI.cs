using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    [Header("Character Info")]
    public TextMeshProUGUI characterName;
    [SerializeField] Image characterImage;

    [Space(15)]
    [Header("Levels")]
    [SerializeField] Transform currentLevelTransform;

    [Space(15)]
    [Header("Stats")]
    [SerializeField] StatsUI statUI;
    [SerializeField] Button statsInfoBtn;
    [SerializeField] LevelStatsUI levelStatsUI;

    [Space(15)]
    [Header("Weapon")]
    [SerializeField] Image weaponImage;

    [Space(15)]
    [Header("Buttons")]
    [SerializeField] Button levelUpBtn;

    public CharacterInfo charInfo;

    private void Awake()
    {
        levelUpBtn.onClick.AddListener(HandleLevelUpButtonClick);
        statsInfoBtn.onClick.AddListener(HandleStatsInfoButtonClick);
    }

    public void SetPlayerInfo(CharacterInfo info)
    {
        charInfo = info;
        characterName.text = info.name;
        SetCharacterSprite(info.sprite);
        SetWeaponSprite(info.initialWeapon.sprite);
        SetProfileValue();
    }


    public void SetProfileValue()
    {
        var character = DynamicData.Instance.GetCharacter(charInfo.name);
        SetCharacterLevel(character);
        SetCharacterStat(character);
        SetLevelUpButtonInfo(character);
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

        statUI.SetInfo(currentStats, lastStats);
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

    void HandleLevelUpButtonClick()
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
            SetProfileValue();
        }
    }

    public void HandleStatsInfoButtonClick()
    {
        levelStatsUI.Appear();
        levelStatsUI.LoadData();
    }


    public void SetLevelUpButtonInfo(PurchasedCharacterInfo character)
    {
        int level = character != null ? character.level : 0;

        if (level == charInfo.prices.Length)
        {
            return;
        }
        else if (level == 0)
        {
            levelUpBtn.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = "Un lock";
        }
        else if (level == 1)
        {
            levelUpBtn.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = "Level up";
        }

        var price = charInfo.prices[level];
        levelUpBtn.interactable = DynamicData.Instance.Data.coin >= price;
        levelUpBtn.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = price.ToString();

    }
}