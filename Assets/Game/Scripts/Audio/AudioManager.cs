using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        //Dieu kien la de khong bi tao moi AudioManager khi quay ve MainMenu
        //Khi da co dieu kien, neu quay ve MainMenu thi se su dung AudioManager moi, con cai cu thi da duoc Destroy
        if (instance == null)
        {
            SetupAudioManager();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetupAudioManager()
    {
        instance = this;

        //Kieu chuyen Scene thi se khong bien mat
        DontDestroyOnLoad(gameObject);
    }

    public AudioSource menuMusic, bossMusic, completeLevelMusic;
    public AudioSource[] levelTracks;
    public AudioSource[] allSFX;

    public void StopMusic()
    {
        menuMusic.Stop();
        bossMusic.Stop();
        completeLevelMusic.Stop();

        foreach (AudioSource levelTrack in levelTracks)
        {
            levelTrack.Stop();
        }
    }
    public void menuMusicPlay()
    {
        StopMusic();
        menuMusic.Play();
    }

    public void bossMusicPlay()
    {
        StopMusic();
        bossMusic.Play();
    }

    public void completeLevelMusicPlay()
    {
        StopMusic();
        completeLevelMusic.Play();
    }

    public void levelTracksPlay(int levelTrack)
    {
        StopMusic();
        levelTracks[levelTrack].Play();
    }

    public void allSFXPlay(int sfxToPlay)
    {
        allSFX[sfxToPlay].Stop();
        allSFX[sfxToPlay].Play();
    }

    public void allSFXPlayPitched(int sfxToPlay)
    {
        allSFX[sfxToPlay].Stop();
        
        //ptich la do cao cua am thanh, chac la Tram va Bong
        allSFX[sfxToPlay].pitch = Random.Range(0.75f, 1.25f);

        allSFX[sfxToPlay].Play();
    }
}
