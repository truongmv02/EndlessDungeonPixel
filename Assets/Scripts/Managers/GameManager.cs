using MVT.Base.Dungeon;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    GameStarted,
    Playing
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{

    [SerializeField] private List<DungeonLevelSO> levels;
    public CharacterController Player { set; get; }
    public int CurrentLevel { set; get; } = 0;
    public Minimap MiniMap { set; get; }
    public RoomController PreviousRoom { get; private set; }
    public RoomController CurrentRoom { get; private set; }

    public GameResources gameResources;
    public int RoomClear { set; get; }
    public int TotalRoom { set; get; }
    public bool IsWinGame { set; get; }
    //  GameState gameState = GameState.GameStarted;

    protected override void Awake()
    {
        base.Awake();
        var characterSelected = DynamicData.Instance.Data.characterSelect;
        var charInfo = DataManager.Instance.CharacterData.GetInfo(characterSelected);
        var playerPrefab = Resources.Load<CharacterController>(charInfo.prefab);
        Player = Instantiate(playerPrefab);
        Player.Control = Player.gameObject.AddComponent<PlayerController>();
        Player.SetInfo(charInfo);
        SoundManager.Instance.SetData();
    }



    private void Start()
    {
        PlayDungeon();
        Observer.Instance.AddObserver(ConstanVariable.ROOM_CLEAR_KEY, HandleClearRoom);
        Observer.Instance.AddObserver(ConstanVariable.PLAYER_DIE_KEY, HandlePlayerDie);
    }

    private void HandleClearRoom(object currentRoom)
    {
        RoomController room = CurrentRoom as RoomController;
        RoomClear++;
        if (RoomClear == TotalRoom)
        {
            IsWinGame = true;
            OpenGameOverUI();
        }
    }


    public void HandlePlayerDie(object obj)
    {
        OpenGameOverUI();
    }
    public void OpenGameOverUI()
    {
        Player.Movement.CanMove = false;
        LeanTween.delayedCall(2, () => GamePlayUIManager.Instance.gameOverUI.Appear());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayDungeon();
        }
        /* switch (gameState)
         {
             case GameState.GameStarted:
             PlayDungeon();
             gameState = GameState.Playing;
             break;

             case GameState.Playing:
             if (Input.GetKeyDown(KeyCode.R))
             {
                 gameState = GameState.GameStarted;
             }
             break;
         }*/
    }

    void PlayDungeon()
    {
        DungeonBuilder.Instance.GenerateDungeon(levels[CurrentLevel]);
        Player.transform.position = CurrentRoom.spawnInfos[0].position;
        SoundManager.Instance.PlayBackgroundMusic(gameResources.gameplayMusic);
    }


    public void SetCurrentRoom(RoomController room)
    {
        PreviousRoom = CurrentRoom;
        CurrentRoom = room;
    }
}