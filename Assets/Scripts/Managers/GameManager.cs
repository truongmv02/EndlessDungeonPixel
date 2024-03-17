using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public PlayerController Player { set; get; }
    public int CurrentLevel { set; get; } = 1;
}