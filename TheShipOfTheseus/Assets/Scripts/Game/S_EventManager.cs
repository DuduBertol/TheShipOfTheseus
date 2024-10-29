using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EventManager : MonoBehaviour
{
    public static S_EventManager Instance {get; private set;}

<<<<<<< HEAD

    [Header("LOVE EVENTS")]
    [SerializeField] private Transform loveSecretRoom;
    [SerializeField] private Transform wallToDestroy;


=======
>>>>>>> d5c657cd1b61de5981f5aafbaa86067b3389d56f
    private void Awake() 
    {
        Instance = this;    
    }

    //==============================================================================
    
    //==============================================================================

    #region GENERAL EVENTS
    
    public void OpenFinalDoor()
    {

    }

    #endregion

    //==============================================================================
    
    //==============================================================================

    #region ENERGY EVENTS
    public void Energy_SpawnLetter()
    {
        Debug.Log("SPAWN - Energy Letter na Porta Principal!");

    }
    public void Energy_SpawnLoveKey()
    {
        Debug.Log("SPAWN - LOVE Key!");

    }
    #endregion

    //==============================================================================
    
    //==============================================================================

    #region LOVE EVENTS
<<<<<<< HEAD
    public void Love_SpawnLoveSecretRoom()
    {
        Debug.Log("SPAWN - Sala Secreta LOVE!");

        loveSecretRoom.gameObject.SetActive(true);
        if(wallToDestroy != null) Destroy(wallToDestroy.gameObject);
    }
=======
>>>>>>> d5c657cd1b61de5981f5aafbaa86067b3389d56f
    





    #endregion

    //==============================================================================
    
    //==============================================================================

    #region LIBRARY EVENTS
<<<<<<< HEAD

=======
    public void Library_SpawnLoveSecretRoom()
    {
        Debug.Log("SPAWN - Sala Secreta LOVE!");

    }
>>>>>>> d5c657cd1b61de5981f5aafbaa86067b3389d56f
    public void Library_OpenSecretBookshelf()
    {
        Debug.Log("Aberta a Sala Secreta LIBRARY!");

    }
    #endregion

    //==============================================================================
    
    //==============================================================================

    #region PLANETS EVENTS
    public void Planets_SpawnSUNKey()
    {
        Debug.Log("SPAWN - SUN Key!");

    }
    public void Planets_SpawnMOONKey()
    {
        Debug.Log("SPAWN - MOON Key!");

    }
    #endregion

}
