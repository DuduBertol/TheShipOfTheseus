using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}

    public bool IsGameStarted;
    public bool IsGamePaused;
    public Volume globalVolume;
    [SerializeField] private int objectsInBoardAmount;
    [SerializeField] private PickUp pickUp;
    [SerializeField] private Transform roofToDestroy;
    [SerializeField] private Transform roofToActive;
    [SerializeField] private Transform keyPosToSpawn;
    [SerializeField] private Transform keyObject;
    [SerializeField] private Transform doorOne;
    [SerializeField] private Transform doorTwo;
    [SerializeField] private Transform doorThree;
    [SerializeField] private Transform doorFour;
    [SerializeField] private Transform doorFive;

    private bool hasPlayedEventOne;
    private bool hasPlayedEventThree;
    private bool hasPlayedEventFive;
    private bool hasPlayedEventSeven;
    private bool hasPlayedEventNine;
    // private bool hasKey;

    private void Awake() 
    {
        Instance = this;    
    }
    private void Start() 
    {  
        // StartCutscene();
    }

    private void Update() 
    {
        if(!IsGameStarted)
        {
            // UpdateCutscene();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            ShakeScreen();
        }
    }

    public void IncreaseObjectsInBoardAmount()
    {
        objectsInBoardAmount++;
        
        if(objectsInBoardAmount == 1 && !hasPlayedEventOne) // DESTROY PLANKS
        {
            hasPlayedEventOne = true;
            PlayEventOne();
        }
        if(objectsInBoardAmount == 3 && !hasPlayedEventThree) // DESTROY PLANKS
        {
            hasPlayedEventThree = true;
            PlayEventThree();
        }
        if(objectsInBoardAmount == 5 && !hasPlayedEventFive) // DESTROY PLANKS
        {
            hasPlayedEventFive = true;
            PlayEventFive();
        }
        if(objectsInBoardAmount == 7 && !hasPlayedEventSeven) // DESTROY PLANKS
        {
            hasPlayedEventSeven = true;
            PlayEventSeven();
        }
        if(objectsInBoardAmount == 9 && !hasPlayedEventNine) // END GAME
        {
            hasPlayedEventNine = true;
            PlayEventNine();
        }
    }
    public void DecreaseObjectsInBoardAmount()
    {
        objectsInBoardAmount--;
    }

    private void PlayEventOne()
    {
        doorOne.gameObject.GetComponent<Animator>().Play("OpenDoor");
        SoundManager.Instance.PlayDoorSound(doorOne.transform.position, 1f);
    }
    private void PlayEventThree()
    {
        doorTwo.gameObject.GetComponent<Animator>().Play("OpenDoor");
        SoundManager.Instance.PlayDoorSound(doorTwo.transform.position, 1f);
    }
    private void PlayEventFive()
    {
        doorThree.gameObject.GetComponent<Animator>().Play("OpenDoor");
        
        SoundManager.Instance.PlayDoorSound(doorThree.transform.position, 1f);
    }
    private void PlayEventSeven()
    {
        Destroy(roofToDestroy.gameObject);
        roofToActive.gameObject.SetActive(true);
        ShakeScreen();
        
        SoundManager.Instance.PlayWoodBreakSound(roofToActive.transform.position, 1f);
    }
    private void PlayEventNine()
    {
        Transform keyTransfom = Instantiate(keyObject, keyPosToSpawn); 
        // hasKey = true;

        SoundManager.Instance.PlayKeySound(roofToActive.transform.position, 1f);
    }

    public void PlayFinalEvent()
    {
        doorFour.gameObject.GetComponent<Animator>().Play("OpenDoor");
        doorFive.gameObject.GetComponent<Animator>().Play("OpenDoor");

        SoundManager.Instance.PlayLockerSound(roofToActive.transform.position, 1f);
        SoundManager.Instance.PlayDoorSound(roofToActive.transform.position, 2f);

        IsGamePaused = true; // GAME OVER
    }

    private void ShakeScreen()
    {
        CameraShaker.Instance.ShakeOnce(6f, 6f, 1f, 3f);
    }

    public void ShowLens(bool value)
    {
        LensDistortion lens;

        globalVolume.profile.TryGet<LensDistortion>(out lens);

        lens.active = value;
    }

    

}
