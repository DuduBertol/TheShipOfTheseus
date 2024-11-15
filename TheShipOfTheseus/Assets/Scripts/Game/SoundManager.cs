using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}

    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;
    [Range(0f, 5f)][SerializeField] private float volume;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource musicAudioSource;

    private float maxVolume = 5f;

    private void Awake() 
    {
        Instance = this;   
    }

    private void Start() 
    {
        volumeSlider.value = volume/maxVolume;

        musicAudioSource.volume = volume/2;

        if(musicAudioSource.clip != null)
        {
            musicAudioSource.Play();
            musicAudioSource.loop = true;
        }    
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void SetVolumeSlider()
    {
        volume = volumeSlider.value * maxVolume;

        musicAudioSource.volume = volume/2;
    }

    //==========================

    public void PlayOpenChestSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_OpenChest, position, volumeMultiplier * volume);
    }

    public void PlayOpenDoorSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_OpenDoor, position, volumeMultiplier * volume);
    }

    public void PlayButtonSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Button, position, volumeMultiplier * volume);
    }
    
    public void PlayCorrectSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Correct, position, volumeMultiplier * volume);
    }
    public void PlayUnlockDoorSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_UnlockDoor, position, volumeMultiplier * volume);
    }
    public void PlayDropSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Drop, position, volumeMultiplier * volume);
    }
    public void PlayEnergyOnSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_EnergyOn, position, volumeMultiplier * volume);
    }
    public void PlayDrawerSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Drawer, position, volumeMultiplier * volume);
    }
    public void PlayGearSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Gear, position, volumeMultiplier * volume);
    }
    public void PlayInteractSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Interact, position, volumeMultiplier * volume);
    }
    public void PlayLeverSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Lever, position, volumeMultiplier * volume);
    }
    
    public void PlayLockerSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Locker, position, volumeMultiplier * volume);
    }
    
    public void PlayPaperSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Paper, position, volumeMultiplier * volume);
    }


    public void PlayFootstepsSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Footsteps, position, volumeMultiplier * volume);
    }
    
    public void PlayPickUpSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_PickUp, position, volumeMultiplier * volume);
    }
    
    public void PlayCreakSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_Creak, position, volumeMultiplier * volume);
    }
    
    public void PlayGetKeySound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_GetKey, position, volumeMultiplier * volume);
    }
    
    public void PlayBookshelftSlideSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.SFX_BookshelfSlide, position, volumeMultiplier * volume);
    }
}
