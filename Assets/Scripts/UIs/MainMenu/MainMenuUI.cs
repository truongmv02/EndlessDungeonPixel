using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : UIBase
{
    public Button characterBtn;
    public Button settingBtn;
    public Button playBtn;

    [SerializeField] GameObject characterHolder;
    [SerializeField] Animator characterAnimator;

    protected void Awake()
    {
        settingBtn.onClick.AddListener(HandleSettingButtonClick);
        characterBtn.onClick.AddListener(HandleCharacterButtonClick);
        playBtn.onClick.AddListener(HandlePlayButtonClick);

        ShowCharacter();
    }

    public override void Appear()
    {
        base.Appear();
        ShowCharacter();
    }

    void ShowCharacter()
    {
        characterHolder.SetActive(true);
        var info = DataManager.Instance.CharacterData.GetInfo(DynamicData.Instance.Data.characterSelect);
        var animatorController = Resources.Load<RuntimeAnimatorController>(info.runtimeAnimatorController);
        characterAnimator.runtimeAnimatorController = animatorController;
    }

    public override void Disappear()
    {
        base.Disappear();
    }


    void HandleCharacterButtonClick()
    {
        Disappear();
        LeanTween.delayedCall(0.2f, () =>
        {
            MainMenuUIManager.Instance.characterSelectionUI.Appear();
        });
    }

    void HandleSettingButtonClick()
    {

        MainMenuUIManager.Instance.settingUI.Appear();
    }

    void HandlePlayButtonClick()
    {
        MainMenuUIManager.Instance.loadingUI.LoadScene("GamePlay");
    }

}
