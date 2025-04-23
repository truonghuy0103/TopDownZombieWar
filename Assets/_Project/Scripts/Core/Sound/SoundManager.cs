using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : SingletonMono<SoundManager>
{
    public bool isMute = false;
    [SerializeField] private List<SoundItem> _soundFxItems = new List<SoundItem>();

    private Dictionary<SoundFXIndex, SoundItem> _dicSoundFxs = new Dictionary<SoundFXIndex, SoundItem>();

    [SerializeField] private List<AudioClip> _lsBGMs = new List<AudioClip>();
    [SerializeField] private AudioSource _bgmSource;
    
    [SerializeField] private List<AudioSource> _listZomNomalSource = new List<AudioSource>();
    private int _currentZomNormal = 0;
    private int _maxFXZomNormal = 4;
    
    [SerializeField] private List<AudioSource> _listZomBossSource = new List<AudioSource>();
    private int _currentZomBoss = 0;
    private int _maxFXZomBoss = 2;
    
    public void Init(Action callback = null)
    {
        _dicSoundFxs.Clear();
        for (int i = 0; i < _soundFxItems.Count; i++)
        {
            Debug.Log("dict: " + _soundFxItems[i].soundFxIndex);
            _dicSoundFxs.Add(_soundFxItems[i].soundFxIndex, _soundFxItems[i]);
        }

        if (callback != null)
        {
            callback.Invoke();
        }
    }

    private SoundItem GetSoundItems(SoundFXIndex soundFxIndex)
    {
        return _soundFxItems.Find(x => x.soundFxIndex == soundFxIndex);
    }

    #region BGM

    public void AddSoundBGM(AudioClip bgmClip)
    {
        if (isMute) return;
        _lsBGMs.Clear();
        _lsBGMs.Add(bgmClip);
    }

    public float GetLengthBGM()
    {
        Debug.Log("count: " + _lsBGMs.Count);
        if (_lsBGMs.Count > 0)
        {
            Debug.Log("length: " + _lsBGMs[0].length);
            return _lsBGMs[0].length;
        }

        return 0;
    }

    public void PlaySoundBGM(float volume = 1, bool isLoop = false)
    {
        _bgmSource.clip = _lsBGMs[0];
        _bgmSource.Play();
        _bgmSource.volume = 0;
        _bgmSource.DOFade(volume, 0.25f);
    }

    public void PlaySoundBGM(SoundBGM soundBGM, float volume = 1, bool isLoop = false)
    {
        _bgmSource.clip = _lsBGMs[(int)soundBGM];
        _bgmSource.Play();
        _bgmSource.volume = 0;
        _bgmSource.DOFade(volume, 0.25f);
        
    }

    public void PauseSoundBGM()
    {
        _bgmSource.Pause();
    }

    public void ResumeSoundBGM()
    {
        _bgmSource.Play();
    }

    public void StopSoundBGM()
    {
        _bgmSource.Stop();
    }

    public float GetCurrentTimeSoundBGM()
    {
        if (_bgmSource.clip != null)
        {
            return _bgmSource.time;
        }

        return 0;
    }

    #endregion

    #region SFX

    public void PlaySoundSFX(SoundFXIndex soundIndex, bool isLoop = false)
    {
        if (isMute) return;

        EazySoundManager.PlaySound(_dicSoundFxs[soundIndex].soundFxClip, isLoop);
    }
    
    public void PlaySoundSFX(SoundFXIndex soundIndex, float volume)
    {
        if (isMute) return;

        EazySoundManager.PlaySound(_dicSoundFxs[soundIndex].soundFxClip, volume);
    }

    public void StopSoundSFX(SoundFXIndex soundIndex)
    {
        Audio audio = EazySoundManager.GetAudio(_dicSoundFxs[soundIndex].soundFxClip);
        if (audio != null)
        {
            audio.Stop();
        }
    }

    public void StopAllSoundFX()
    {
        EazySoundManager.StopAllSounds();
        
        foreach (var autioSource in _listZomNomalSource)
        {
            autioSource.Stop();
        }
        
        foreach (var autioSource in _listZomBossSource)
        {
            autioSource.Stop();
        }
    }

    public bool CheckSoundFXAvailable(SoundFXIndex soundIndex)
    {
        Audio audio = EazySoundManager.GetAudio(_dicSoundFxs[soundIndex].soundFxClip);
        if (audio != null && audio.IsPlaying)
        {
            return true;
        }

        return false;
    }

    

    #endregion

    public void Mute()
    {
        isMute = true;
        StopSoundBGM();
        StopAllSoundFX();
    }

    public void UnMute()
    {
        isMute = false;
        for (SoundFXIndex i = SoundFXIndex.Click; i < SoundFXIndex.COUNT; i++)
        {
            PlaySoundSFX(i);
        }
    }

    public void PauseAll()
    {
        EazySoundManager.PauseAllSounds();
    }

    public void ResumeAll()
    {
        EazySoundManager.ResumeAllSounds();
    }

    public void PlaySoundZombie(SoundFXIndex soundFXIndex)
    {
        SoundItem soundItem = GetSoundItems(soundFXIndex);
        switch (soundFXIndex)
        {
            case SoundFXIndex.ZombieNormal:
            {
                if (_currentZomNormal < _maxFXZomNormal)
                {
                    AudioSource audio = _listZomNomalSource[_currentZomNormal];
                    audio.clip = soundItem.soundFxClip;
                    audio.volume = 0.05f;
                    audio.loop = true;
                    audio.Play();

                    _currentZomNormal++;
                }

                break;
            }

            case SoundFXIndex.ZombieBoss:
            {
                if (_currentZomBoss < _maxFXZomBoss)
                {
                    AudioSource audio = _listZomBossSource[_currentZomBoss];
                    audio.clip = soundItem.soundFxClip;
                    audio.volume = 0.1f;
                    audio.loop = true;
                    audio.Play();

                    _currentZomBoss++;
                }

                break;
            }
        }
    }

    public void StopSoundZombie(SoundFXIndex soundFXIndex)
    {
        SoundItem soundItem = GetSoundItems(soundFXIndex);
        switch (soundFXIndex)
        {
            case SoundFXIndex.ZombieNormal:
            {
                if (_currentZomNormal > 0)
                {
                    _currentZomNormal--;
                    if (_currentZomNormal < _listZomNomalSource.Count)
                    {
                        _listZomNomalSource[_currentZomNormal].Stop();
                    }
                }
                break;
            }
            case SoundFXIndex.ZombieBoss:
            {
                if (_currentZomBoss > 0)
                {
                    _currentZomBoss--;
                    if (_currentZomBoss < _listZomBossSource.Count)
                    {
                        _listZomBossSource[_currentZomBoss].Stop();
                    }
                }
                break;
            }
        }
    }
}

public enum SoundFXIndex
{
    Click = 0,
    ZombieNormal,
    ZombieBoss,
    Rifle,
    Shotgun,
    RifleReload,
    ShotgunReload,
    SwitchWeapon,
    COUNT
}

public enum SoundBGM
{
    MissionOne = 0,
    MissionTwo = 1,
    COUNT
}

[Serializable]
public class SoundItem
{
    public SoundFXIndex soundFxIndex;
    public AudioClip soundFxClip;
}


