using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}

    [SerializeField] private int objectsInBoardAmount;
    [SerializeField] private PickUp pickUp;
    [SerializeField] private Transform roofToDestroy;
    [SerializeField] private Transform glassesPosToSpawn;
    [SerializeField] private Transform glassesObject;
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

    private void Awake() 
    {
        Instance = this;    
    }

    /* private void Start() 
    {
        pickUp.OnAnyObjectDroppedOnBoard += PickUp_OnAnyObjectDroppedOnBoard;
    }

    private void PickUp_OnAnyObjectDroppedOnBoard(object sender, PickUp.OnAnyObjectDroppedOnBoardEventArgs e)
    {
        if(e.boardObject.GetIsMainObject())
        {
            objectsInBoardAmount++;

            if(objectsInBoardAmount == 7 && !hasPlayedEventSeven) // DESTROY PLANKS
            {
                hasPlayedEventSeven = true;
            }
            if(objectsInBoardAmount == 9 && !hasPlayedEventNine) // END GAME
            {
                hasPlayedEventNine = true;
            }
        }
    } */

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
        // open doorOne
    }
    private void PlayEventThree()
    {
        doorTwo.gameObject.GetComponent<Animator>().Play("OpenDoor");
        // open doorTwo
    }
    private void PlayEventFive()
    {
        doorThree.gameObject.GetComponent<Animator>().Play("OpenDoor");
        // open doorThree
    }
    private void PlayEventSeven()
    {
        Destroy(roofToDestroy.gameObject);
        //Cair tábua
        //Play sound
    }
    private void PlayEventNine()
    {
        Transform glassesTransfom = Instantiate(glassesObject, glassesPosToSpawn); //Cair monóculo
        doorFour.gameObject.GetComponent<Animator>().Play("OpenDoor");
        doorFive.gameObject.GetComponent<Animator>().Play("OpenDoor");
        // open doorFinal
        //Play sound
    }
}
