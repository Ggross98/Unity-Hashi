using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : SingletonMonoBehaviour<SoundController>
{
    private AudioSource bgm;
    private AudioClip[] bgmClips = null;
    private int bgmIndex = 0;
    private AudioSource[] se;
    private int maxSEcount = 3;

    private bool playingBGM = false;
    public bool isMuted = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(bgm == null) bgm = gameObject.AddComponent<AudioSource>();
        bgm.loop = false;
        if(se == null){
            se = new AudioSource[maxSEcount];
            for(int i = 0; i< maxSEcount; i++){
                se[i] = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    void Update()
    {
        if(playingBGM){
            if(!bgm.isPlaying){
                bgmIndex ++;
                if(bgmIndex >= bgmClips.Length) bgmIndex = 0;

                PlayBGMAtIndex(bgmIndex);
            }
        }
    }

    public void Mute(bool b){
        isMuted = b;
        SetVolume(b?0 :100);
    }

    public void ChangeMute(){
        Mute(!isMuted);
    }


    private void SetVolume(float v){
        bgm.volume = v;
        foreach(var s in se) s.volume = v;
    }

    public void SetBGM(AudioClip[] clips){
        // Debug.Log("Set bgm");
        // if(bgmClips == clips) return;
        
        bgmClips = clips;

        if(playingBGM){
            // bgmIndex = -1;
        }else{
            PlayBGMAtIndex(0);
        }
        
        
    }

    public int GetBGMCount(){
        if(bgmClips == null) return 0;
        else return bgmClips.Length;
    }

    private void PlayBGM(AudioClip clip){
        bgm.clip = clip;
        bgm.Play();
        playingBGM = true;
    }

    public void PlayBGMAtIndex(int i){
        if(bgmClips == null) return;
        if(i < 0 || i >= bgmClips.Length) return;
        PlayBGM(bgmClips[i]);
        bgmIndex = i;
        
    }

    public void PlaySoundEffect(AudioClip clip){
        int index = 0;
        for(int i = 0; i < maxSEcount; i++){
            if(!se[i].isPlaying){
                index = i;
                break;
            }
        }
        var source = se[index];
        //source.Stop();
        source.clip = clip;
        source.Play();

    }
}
