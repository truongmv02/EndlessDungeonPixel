using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSelectionUI : UIBase
{
    public CharacterSelector characterSelectorPrefab;
    public Transform content;

    [SerializeField] SwipeController swipeController;
    [SerializeField] Text characterName;
    [SerializeField] Button infoBtn;
    [SerializeField] Button selectBtn;
    [SerializeField] Button unLockBtn;
    [SerializeField] Button backBtn;

    List<CharacterInfo> characterInfoList;
    List<CharacterSelector> characterSelectorList;
    CanvasGroup canvasGroup;

    CharacterSelector characterSelected;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        infoBtn.onClick.AddListener(HandleInfoButtonClick);
        unLockBtn.onClick.AddListener(HandleUnlockButtonClick);
        selectBtn.onClick.AddListener(HandleSelectButtonClick);
        backBtn.onClick.AddListener(BackToMainUI);
        swipeController.OnChangeValue += OnChangeCharacter;
        LoadData();
    }

    public void SetActiveOpacity(float opacity, float time)
    {
        bool active = !(opacity == 0f);
        if (active)
        {
            gameObject.SetActive(true);
        }
        else
        {
            LeanTween.delayedCall(0.3f, () =>
            {
                gameObject.SetActive(false);
            });
        }
        LeanTween.alphaCanvas(canvasGroup, opacity, time);
    }

    private void LoadData()
    {
        characterInfoList = DataManager.Instance.CharacterData.GetAllInfo();
        characterSelectorList = new();

        swipeController.MaxPage = characterInfoList.Count;

        for (int i = 0; i < characterInfoList.Count; i++)
        {
            var charInfo = characterInfoList[i];
            var characterSelector = Instantiate(characterSelectorPrefab, content);
            characterSelector.Info = charInfo;
            characterSelector.gameObject.name = charInfo.name;
            characterSelector.SetCharacterSprite(charInfo.sprite);
            characterSelector.UnSelect();
            characterSelectorList.Add(characterSelector);
            if (DynamicData.Instance.Data.characterSelect == charInfo.name)
            {
                characterSelected = characterSelector;
                characterSelector.Select();
                swipeController.CurrentPage = i;
                characterName.text = charInfo.name;
            }
        }
        swipeController.MovePage();
    }

    void BackToMainUI()
    {
        Disappear();
        LeanTween.delayedCall(0.2f, () =>
        {
            MainMenuUIManager.Instance.mainMenuUI.Appear();
        });
    }

    void HandleInfoButtonClick()
    {
        MainMenuUIManager.Instance.characterInfoUI.SetInfo(characterSelected.Info);
        MainMenuUIManager.Instance.characterInfoUI.Appear();
    }

    void HandleSelectButtonClick()
    {
        DynamicData.Instance.SelectCharacter(characterSelected.Info.name);
        BackToMainUI();
    }

    void HandleUnlockButtonClick()
    {
        bool result = DynamicData.Instance.AddPurchasedCharacter(characterSelected.Info.name);
        if (result)
        {
            MainMenuUIManager.Instance.SetCoinText();
            selectBtn.gameObject.SetActive(true);
            unLockBtn.gameObject.SetActive(false);
        }
    }

    void OnChangeCharacter(int index)
    {
        if (characterSelected)
        {
            characterSelected.UnSelect();
        }
        characterSelected = characterSelectorList[index];
        characterSelected.Select();
        characterName.text = characterSelected.name;
        SetButton();
    }

    public void SetButton()
    {
        var charInfo = characterSelected.Info;
        var character = DynamicData.Instance.GetCharacter(charInfo.name);

        if (character == null)
        {
            selectBtn.gameObject.SetActive(false);
            unLockBtn.gameObject.SetActive(true);
            SetUnlockButtonData();
        }
        else
        {
            selectBtn.gameObject.SetActive(true);
            unLockBtn.gameObject.SetActive(false);
        }
    }

    void SetUnlockButtonData()
    {
        int price = characterSelected.Info.prices[0];
        int currentPrice = DynamicData.Instance.Data.coin;
        unLockBtn.interactable = currentPrice >= price;
        unLockBtn.transform.Find("PriceHolder").Find("Value").GetComponent<Text>().text = price.ToString();
    }


    private void OnDestroy()
    {
        swipeController.OnChangeValue -= OnChangeCharacter;
    }
}
