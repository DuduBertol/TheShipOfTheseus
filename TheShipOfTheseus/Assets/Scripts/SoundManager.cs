using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;
    [Range(1f, 10f)][SerializeField] private float volume; 

    private void Awake() 
    {
        Instance = this;     
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    //

    public void PlayFootstepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.footsteps, position, volumeMultiplier * volume);
    }

    public void PlayPaperSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.paper, position, volumeMultiplier * volume);
    }

    public void PlayBedSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.bed, position, volumeMultiplier * volume);
    }
    public void PlayDoorSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.door, position, volumeMultiplier * volume);
    }

    public void PlayKeySound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.key, position, volumeMultiplier * volume);
    }

    public void PlayLockerSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.locker, position, volumeMultiplier * volume);
    }

    public void PlayPinSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.pin, position, volumeMultiplier * volume);
    }

    public void PlayThrowSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.throwObj, position, volumeMultiplier * volume);
    }

    public void PlayWoodBreakSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.woodBreak, position, volumeMultiplier * volume);
    }

    /* public void PlayAmbienceMusic(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.musicAmbience, position, volumeMultiplier * volume);
    } */

}
