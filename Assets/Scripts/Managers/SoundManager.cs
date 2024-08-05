using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [Header("Main Setting:")]
    [Range(0, 1)]
    [SerializeField] float musicVolume = 1f;
    [Range(0, 1)]
    [SerializeField] float sfxVolume = 1f;

    public AudioSource music;
    public AudioSource sfx;

    public void SetData()
    {
        SetSFXVolume(DynamicData.Instance.Data.sfxVolume);
        SetMusicVolume(DynamicData.Instance.Data.musicVolume);
    }

    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip, sfxVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        sfx.volume = sfxVolume;
    }
    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        music.volume = musicVolume;
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        music.clip = clip;
        music.loop = loop;
        music.volume = musicVolume;
        music.Play();
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        PlayMusic(clip);
    }
}