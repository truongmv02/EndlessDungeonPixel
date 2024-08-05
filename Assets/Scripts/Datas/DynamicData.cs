using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicData : Singleton<DynamicData>
{
    public GameDataDefault Data { set; get; }

    private const string CoinKey = "Coin";
    private const string PurchasedCharactersKey = "PurchasedCharacters";
    private const string CharacterSelectKey = "CharacterSelect";
    private const string SFXVolumeKey = "SFXVolume";
    private const string MusicVolumeKey = "MusicVolume";

    public DynamicData()
    {
        LoadData();
    }

    public void LoadData()
    {
        if (!PlayerPrefs.HasKey(CoinKey))
        {
            SetDataDefault();
            return;
        }

        Data = new GameDataDefault();
        Data.coin = PlayerPrefs.GetInt(CoinKey);
        Data.characterSelect = PlayerPrefs.GetString(CharacterSelectKey);
        Data.sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
        Data.musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
        string charactersString = PlayerPrefs.GetString(PurchasedCharactersKey);
        Data.characters = JsonConvert.DeserializeObject<List<PurchasedCharacterInfo>>(charactersString);

    }

    private void SetDataDefault()
    {
        var dataString = Resources.Load<TextAsset>("Data/Default/GameDataDefault").text;
        Data = JsonConvert.DeserializeObject<GameDataDefault>(dataString);

        SetCoin(Data.coin);
        SelectCharacter(Data.characterSelect);
        SavePurchasedCharacters();
    }

    public void SetSFXVolume(float value)
    {
        Data.sfxVolume = value;
        PlayerPrefs.SetFloat(SFXVolumeKey, value);
    }

    public void SetMusicVolume(float value)
    {
        Data.musicVolume = value;
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
    }


    public void SetCoin(int coin)
    {
        Data.coin = coin;
        PlayerPrefs.SetInt(CoinKey, coin);
    }
    public void SelectCharacter(string name)
    {
        Data.characterSelect = name;
        PlayerPrefs.SetString(CharacterSelectKey, name);
    }

    public PurchasedCharacterInfo GetCharacter(string name)
    {
        return Data.characters.Find(x => x.name == name);
    }

    public bool UpgradeCharacter(string name)
    {
        CharacterInfo info = DataManager.Instance.CharacterData.GetInfo(name);
        var character = GetCharacter(name);
        int price = info.prices[character.level - 1];
        if (Data.coin < price) return false;
        SetCoin(Data.coin - price);
        character.level++;
        SavePurchasedCharacters();

        return true;
    }

    public void SavePurchasedCharacters()
    {
        var dataString = JsonConvert.SerializeObject(Data.characters);
        PlayerPrefs.SetString(PurchasedCharactersKey, dataString);
    }

    public bool AddPurchasedCharacter(string name)
    {
        CharacterInfo info = DataManager.Instance.CharacterData.GetInfo(name);
        if (Data.coin < info.prices[0]) return false;
        PurchasedCharacterInfo character = new PurchasedCharacterInfo() { name = name, level = 1 };
        Data.characters.Add(character);
        SetCoin(Data.coin - info.prices[0]);
        SavePurchasedCharacters();
        return true;
    }

}
