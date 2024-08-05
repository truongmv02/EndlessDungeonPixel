using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIBase
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public Button closeBtn;


    private void Awake()
    {
        closeBtn.onClick.AddListener(HandleCloseButtonClick);

        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
    }

    private void Start()
    {
        musicSlider.value = DynamicData.Instance.Data.musicVolume;
        sfxSlider.value = DynamicData.Instance.Data.sfxVolume;
    }

    void ChangeMusicVolume(float value)
    {
        DynamicData.Instance.SetMusicVolume(value);
        SoundManager.Instance.SetMusicVolume(value);
    }

    void ChangeSFXVolume(float value)
    {
        DynamicData.Instance.SetSFXVolume(value);
        SoundManager.Instance.SetSFXVolume(value);
    }

    void HandleCloseButtonClick()
    {
        Disappear();
    }




}
