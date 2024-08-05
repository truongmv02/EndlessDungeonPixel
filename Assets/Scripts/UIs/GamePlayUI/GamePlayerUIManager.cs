using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUIManager : SingletonMonoBehaviour<GamePlayUIManager>
{
    public PauseUI pauseUI;
    public GameOverUI gameOverUI;
    public SettingUI settingUI;
    public JoystickBase playerJoystick;
    public HpBossUI hpBossUI;

    [SerializeField] Button pauseBtn;


    protected override void Awake()
    {
        base.Awake();

        pauseBtn.onClick.AddListener(HandlePauseButtonClick);
    }

    void HandlePauseButtonClick()
    {
        Time.timeScale = 0;
        pauseUI.Appear();
    }
}
