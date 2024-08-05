using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GameResources", menuName = "Scriptable Objects/GameResoucres")]
public class GameResources : ScriptableObject
{
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    public AudioClip enemyDie;
}
