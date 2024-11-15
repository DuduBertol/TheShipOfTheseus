using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_EventManager : MonoBehaviour
{
    public static S_EventManager Instance {get; private set;}

    public event EventHandler OnGameOver;
    public event EventHandler OnToggleIsPaused;

    [Header("GAME EVENTS")]
    public bool isGameStarded;
    public bool isGameOver;
    public bool isPaused;

    [Header("ROOM EVENTS")]
    [SerializeField] private Transform lightBedsideTable;
    [SerializeField] private Transform lightPhoto;

    [Header("ENERGY EVENTS")]
    [SerializeField] private Transform loveKey;
    [SerializeField] private Transform loveKeyImage;
    [SerializeField] private Transform energyCard;

    [Header("LOVE EVENTS")]
    [SerializeField] private Transform loveSecretRoom;
    [SerializeField] private Transform wallToDestroy;
    
    [Header("PLANETS EVENTS")]
    [SerializeField] private Transform sunKey;
    [SerializeField] private Transform moonKey;


    private void Awake() 
    {
        Instance = this;    
    }
    private void Start() 
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleIsPaused();    
    }

    //==============================================================================
    
    //==============================================================================

    #region GENERAL EVENTS

    public void SetGameOver()
    {
        Debug.Log("Game over! - Event");

        isGameOver = true;

        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    public void ToggleIsPaused()
    {
        if(isGameStarded && !isGameOver)
        {
            isPaused = !isPaused;

            OnToggleIsPaused?.Invoke(this, EventArgs.Empty);

            if(isPaused)
            {
                Cursor.lockState = CursorLockMode.Confined;    
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;    
                Cursor.visible = false;
            }
        }   
    }


    public void DestroyLightsMenu()
    {
        lightBedsideTable.gameObject.SetActive(false);
        lightPhoto.gameObject.SetActive(false);
    }

    #endregion

    //==============================================================================
    
    //==============================================================================

    #region ENERGY EVENTS
    public void Energy_SpawnLetter()
    {
        Debug.Log("SPAWN - Energy Letter na Porta Principal!");
        energyCard.gameObject.SetActive(true);
    }
    public void Energy_SpawnLoveKey()
    {
        Debug.Log("SPAWN - LOVE Key!");
        loveKey.gameObject.SetActive(true);
        loveKeyImage.gameObject.SetActive(true);
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
        sunKey.gameObject.SetActive(true);

        loveKey.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        loveKeyImage.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void Planets_SpawnMOONKey()
    {
        Debug.Log("SPAWN - MOON Key!");
        moonKey.gameObject.SetActive(true);
    }
    #endregion

    

    //==============================================================================
    
    //==============================================================================

    #region SCENE EVENTS

    public void LoadLoadingScene()
    {
        SceneManager.LoadScene("SN_MainMenu");
    }

    #endregion

    public void PrintTeste()
    {
        Debug.Log("Funcionando!");
    }

}
