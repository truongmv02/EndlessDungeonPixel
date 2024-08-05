using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : UIBase
{

    [SerializeField] Button homeBtn;
    [SerializeField] TextMeshProUGUI levelCompletion;
    [SerializeField] TextMeshProUGUI coinValue;

    private void Start()
    {
        homeBtn.onClick.AddListener(HandleHomeButtonClick);
    }

    public override void Appear()
    {
        base.Appear();
        int minCoin = 500;
        int roomClear = GameManager.Instance.RoomClear;
        int totalRoom = GameManager.Instance.TotalRoom;
        int coin = roomClear * minCoin;
        coinValue.text = "+" + coin.ToString();
        levelCompletion.text = roomClear.ToString() + "/" + totalRoom.ToString();
        coin += DynamicData.Instance.Data.coin;
        DynamicData.Instance.SetCoin(coin);



    }

    void HandleHomeButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
}
