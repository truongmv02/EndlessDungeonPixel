using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : UIBase
{
    [SerializeField] Button homeBtn;
    [SerializeField] Button resumeBtn;
    [SerializeField] Button settingBtn;

    private void Awake()
    {
        homeBtn.onClick.AddListener(HandleHomeButtonClick);
        resumeBtn.onClick.AddListener(HandleResumeButtonClick);
        settingBtn.onClick.AddListener(HandleSettingButtonClick);
    }

    void HandleHomeButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    void HandleResumeButtonClick()
    {
        Disappear();
        LeanTween.delayedCall(0.2f, () =>
        {
            Time.timeScale = 1;
        }).setIgnoreTimeScale(true);
    }

    void HandleSettingButtonClick()
    {
        GamePlayUIManager.Instance.settingUI.Appear();
    }
}
