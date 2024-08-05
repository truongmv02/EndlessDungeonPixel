using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : SingletonMonoBehaviour<MainMenuUIManager>
{
    public Text coin;

    public MainMenuUI mainMenuUI;
    public CharacterSelectionUI characterSelectionUI;
    public CharacterInfoUI characterInfoUI;
    public SettingUI settingUI;
    public LoadingUI loadingUI;
    [SerializeField] GameResources gameResources;

    protected override void Awake()
    {
        base.Awake();
        mainMenuUI.gameObject.SetActive(true);
        characterSelectionUI.gameObject.SetActive(false);
        characterInfoUI.gameObject.SetActive(false);
        SoundManager.Instance.SetData();
        SetCoinText();
        SoundManager.Instance.PlayBackgroundMusic(gameResources.mainMenuMusic);
    }
    public void SetCoinText()
    {
        coin.text = DynamicData.Instance.Data.coin.ToString();
    }
}
