using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * SwitchMusic(SongName) to change music
 * PlaySE(SEName) to change SE
 * musics and SEs names are listed in Editor
 * */
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public AudioSource MusicAudio;
    public AudioSource MusicAudio2;
    public Music[] MusicPool;
    bool InputInterval = false; //prevent too many input in short time
    Music CurrentMusic;
    Music NextMusic;
    bool VolumeDownToZero = false;

    [Space(30)]
    public AudioSource SEAudio;
    public SE[] SoundEffectPool;

    public void SwitchMusic(string name)
    {
        if (InputInterval == false)
        {
            InputInterval = true;

            CurrentMusic = CurrentPlaying();
            NextMusic = NextPlay(name);
            if (CurrentMusic == null)
            {
                MusicToAudioSource(NextMusic);
                MusicAudio.Play();
                InputInterval = false;
            }
            else
            {
                StartCoroutine(DecreaseVolume()); // change music 
            }
        }
    }
    public Music CurrentPlaying()
    {
        if (MusicAudio.isPlaying)
        {
            foreach (Music s in MusicPool)
            {
                if (s.clip == MusicAudio.clip)
                {
                    return s;
                }
            }
        }
        else if (MusicAudio2.isPlaying)
        {
            foreach (Music s in MusicPool)
            {
                if (s.clip == MusicAudio.clip)
                {
                    return s;
                }
            }
        }
        else return null;

        return null;
    }
    public Music NextPlay(string name)
    {
        Music s = Array.Find(MusicPool, sound => sound.name == name);
        if (s == null)
        {
            return null;
        }
        else return s;
    }
    public void MusicToAudioSource(Music s)
    {
        if (!MusicAudio.isPlaying)
        {
            MusicAudio.clip = s.clip;
            MusicAudio.volume = s.volume;
        }
        else if (!MusicAudio2.isPlaying)
        {
            MusicAudio2.clip = s.clip;
            MusicAudio2.volume = s.volume;
        }
    }
    IEnumerator DecreaseVolume()
    {
        while (true)
        {
            if (MusicAudio.isPlaying)
            {
                if (MusicAudio.volume > 0.4f)
                {
                    MusicAudio.volume -= 0.05f;
                }
                else
                {
                    StartCoroutine(IncreaseVolume());
                    break;
                }
            }
            else if (MusicAudio2.isPlaying)
            {
                if (MusicAudio2.volume > 0.4f)
                {
                    MusicAudio2.volume -= 0.05f;
                }
                else
                {
                    StartCoroutine(IncreaseVolume());
                    break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator IncreaseVolume()
    {
        int changingphase = 0;
        int audionum = 0;          //which audio was using
        float tempVol = NextMusic.volume;
        while (changingphase == 0) // play next music
        {
            MusicToAudioSource(NextMusic);
            if (MusicAudio.isPlaying)
            {
                MusicAudio2.volume = 0.2f;
                MusicAudio2.Play();
                changingphase = 1;
                audionum = 1;
            }
            else if (MusicAudio2.isPlaying)
            {
                MusicAudio.volume = 0.2f;
                MusicAudio.Play();
                changingphase = 1;
                audionum = 2;
            }
            yield return new WaitForSeconds(0.1f);
        }
        while (changingphase == 1) // increase next music vol and stop previous audio
        {
            if (audionum == 1)
            {
                MusicAudio.volume -= 0.05f;
                MusicAudio2.volume += 0.05f;
                if (MusicAudio.volume <= 0f && MusicAudio2.volume >= tempVol)
                {
                    MusicAudio.Stop();
                    InputInterval = false;
                    break;
                }
            }
            else if (audionum == 2)
            {
                MusicAudio2.volume -= 0.05f;
                MusicAudio.volume += 0.05f;
                if (MusicAudio2.volume <= 0f && MusicAudio.volume >= tempVol)
                {
                    MusicAudio2.Stop();
                    InputInterval = false;
                    break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void PlaySE(string name)
    {
        SE se = Array.Find(SoundEffectPool, sound => sound.name == name);
        SEAudio.clip = se.clip;
        SEAudio.volume = se.volume;
        SEAudio.Play();
    }

    /* Test
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchMusic("GamePlay");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaySE("BlowLow");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlaySE("BlowHigh");
        }
    }
    public void Start()
    {
        SwitchMusic("Plot");
        Debug.Log("Play");
    }*/
}
