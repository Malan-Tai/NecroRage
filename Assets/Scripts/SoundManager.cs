using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound {
        Arrow_hit1, Arrow_hit2,
        Throw_arrow1, Throw_arrow2, Throw_arrow3,
        Catapult_throw,
        Rock_explode1, Rock_explode2,
        Damage_necrovore,
        Death_necrovore,
        Dash,
        Death_soldier1, Death_soldier2,
        Eat_flesh_long, Eat_flesh_long_burp,
        Flesh1, Flesh2, Flesh3,
        Blurp,
        Sword_hit1, Sword_hit2, Sword_hit3,
        Validation_menu,
        Movement_menu,
        Back_menu,
        Death_soldier3, Death_soldier4, Death_soldier5, Death_soldier6,
        Death_necrovore3, Death_necrovore4, Death_necrovore5, Death_necrovore6,
        Scream1, Scream2,
        FootSteps1, FootSteps2, FootSteps3,
        Test
    }

    public static Dictionary<Sound,float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;


    public static void PlaySound(Sound sound, Vector3 position, float volume = 1f) {
        if (CanPlaySound(sound)) {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 40f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.volume = volume * SoundAssets.instance.sfxVolumeModifier * SoundAssets.instance.mainSFXVolume;
            audioSource.Play();
            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound, float volume = 1f) {
        if (CanPlaySound(sound)) {
            if (oneShotGameObject == null) {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound),volume * SoundAssets.instance.sfxVolumeModifier * SoundAssets.instance.mainSFXVolume);
        }
        
    }

    private static bool CanPlaySound(Sound sound) {
        switch (sound){
            default:
                return true;
            case Sound.Movement_menu:
                if (soundTimerDictionary.ContainsKey(sound)) {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float movement_menuSoundTimerMax = .1f;
                    if (lastTimePlayed + movement_menuSoundTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    } 
                    else return false;
                }
                else return true;
        }
    }

    public static AudioClip GetAudioClip(Sound sound) {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.instance.soundAudioClipsArray) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound "+ sound  + " not found");
        return null;
    }
}
