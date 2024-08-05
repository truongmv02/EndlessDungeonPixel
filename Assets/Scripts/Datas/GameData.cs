using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PurchasedCharacterInfo
{
    public string name;
    public int level;
}

[System.Serializable]
public class GameDataDefault
{
    public int coin;
    public float sfxVolume;
    public float musicVolume;
    public string characterSelect;
    public List<PurchasedCharacterInfo> characters;
}
