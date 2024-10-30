using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EventManager : MonoBehaviour
{
    public static S_EventManager Instance {get; private set;}


    [Header("LOVE EVENTS")]
    [SerializeField] private Transform loveSecretRoom;
    [SerializeField] private Transform wallToDestroy;


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
    public void Love_SpawnLoveSecretRoom()
    {
        Debug.Log("SPAWN - Sala Secreta LOVE!");

        loveSecretRoom.gameObject.SetActive(true);
        if(wallToDestroy != null) Destroy(wallToDestroy.gameObject);
    }
    





    #endregion

    //==============================================================================
    
    //==============================================================================

    #region LIBRARY EVENTS
    public void Library_SpawnLoveSecretRoom()
    {
        Debug.Log("SPAWN - Sala Secreta LOVE!");

    }
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
