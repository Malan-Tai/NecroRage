using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundAssets : MonoBehaviour
{
    public static SoundAssets instance;

    public SoundAudioClip[] soundAudioClipsArray;

    [System.Serializable]
    public class SoundAudioClip {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }


    public AudioClip mainMusic;
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource ambiantSource;

    private bool firstSourcePlaying;


    public float musicVolumeModifier = 0.35f;
    public float ambiantVolumeModifier = 0.2f;
    public float sfxVolumeModifier = 0.5f;

    public float mainMusicVolume = 1f;
    public float mainAmbiantVolume = 1f;
    public float mainSFXVolume = 1f;

    public Slider musicSlide;
    public Slider ambiantSlide;
    public Slider sfxSlide;


    public void Awake()
    {
        if (instance)
        {
            Debug.Log("Il y a déjà une instance de SoundManager : Autodestruction lancée ");
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        ambiantSource = this.gameObject.AddComponent<AudioSource>();
            
        musicSource.loop = true;
        musicSource2.loop = true;
        musicSource.volume = 0.5f;      // VALEURS DE BASE DES SLIDERS
        musicSource2.volume = 0.5f;
        ambiantSource.volume = 0.5f;
        mainMusicVolume = 0.5f;
        mainAmbiantVolume = 0.5f;
        mainSFXVolume = 0.5f;
        
        PlayMusicWithFade(mainMusic,3f);
        SoundManager.soundTimerDictionary = new Dictionary<SoundManager.Sound, float>();
        SoundManager.soundTimerDictionary.Add(SoundManager.Sound.Movement_menu,0f);



    }

    public void Update()
    {
        
    }

    public void playMunchSound(Vector3 position)
    {
        int soundToPlay = Random.Range(0,3);
        switch (soundToPlay)
        {
            case 0: SoundManager.PlaySound(SoundManager.Sound.Flesh1, position); break;
            case 1: SoundManager.PlaySound(SoundManager.Sound.Flesh2, position); break;
            case 2: SoundManager.PlaySound(SoundManager.Sound.Flesh3, position); break;
            default: break;
        }
    }

    public void playSwordSound(Vector3 position)
    {
        int soundToPlay = Random.Range(0,3);
        switch (soundToPlay)
        {
            case 0: SoundManager.PlaySound(SoundManager.Sound.Sword_hit1, position); break;
            case 1: SoundManager.PlaySound(SoundManager.Sound.Sword_hit2, position); break;
            case 2: SoundManager.PlaySound(SoundManager.Sound.Sword_hit3, position); break;
            default: break;
        }
    }


    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activesource = (firstSourcePlaying) ? musicSource : musicSource2;
        
        activesource.clip = musicClip;
        activesource.volume =  mainMusicVolume * musicVolumeModifier;
    }

    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = musicSource;
        if (!activeSource.isPlaying)
        {
            StartCoroutine(UpdateMusicWithFade(activeSource,newClip,transitionTime));
        }
        
    }

    public IEnumerator StopMusicWithFade(float transitionTime = 1.0f) {

        AudioSource activeSource = (firstSourcePlaying) ? musicSource : musicSource2;
        float t = 0f;

        for (t= 0f; t<= transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume = (1-(t/transitionTime)) * mainMusicVolume * musicVolumeModifier;
            yield return null;
        }

        activeSource.Stop();
    }

    public IEnumerator StopAmbiantWithFade(float transitionTime = 1.0f) {

        AudioSource activeSource = ambiantSource;
        float t = 0f;

        for (t= 0f; t<= transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume = (1-(t/transitionTime)) * mainAmbiantVolume * musicVolumeModifier;
            yield return null;
        }

        activeSource.Stop();
    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)

    {
        AudioSource activeSource = (firstSourcePlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstSourcePlaying) ? musicSource2 : musicSource;
        
        firstSourcePlaying = !firstSourcePlaying;

        newSource.clip = musicClip;
        newSource.Play();

        StartCoroutine(UpdateMusicWithCrossFade(activeSource,newSource,transitionTime));
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        for (t= 0; t< transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume = (1-(t/transitionTime)) *  mainMusicVolume * musicVolumeModifier;
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        for (t= 0; t< transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume = (t/transitionTime) *  mainMusicVolume * musicVolumeModifier;
            yield return null;
        }
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0f;

        for (t= 0f; t<= transitionTime; t+=Time.deltaTime)
        {
            original.volume = (1-(t/transitionTime)) * mainMusicVolume * musicVolumeModifier;
            newSource.volume = (t / transitionTime) * mainMusicVolume * musicVolumeModifier;
            yield return null;
        }

        original.Stop();
    }
   
    public void SetMusicVolume()
    {
        musicSource.volume = musicSlide.value * musicVolumeModifier;
        musicSource2.volume = musicSlide.value * musicVolumeModifier;
        mainMusicVolume = musicSlide.value;
    }

    public void SetAmbiantVolume()
    {
        ambiantSource.volume = ambiantSlide.value * ambiantVolumeModifier;
        mainAmbiantVolume = ambiantSlide.value;
    }

    public void SetSFXVolume()
    {
        mainSFXVolume = sfxSlide.value * sfxVolumeModifier;
    }

    public void ChangeLoop(bool value)
    {
        musicSource.loop = value;
        musicSource2.loop = value; 
    }
}
