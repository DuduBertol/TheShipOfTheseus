using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("In-Game")]
    [SerializeField] private Transform interactPanel;
    [SerializeField] private Transform inspectPanel;

    [SerializeField] private S_PlayerPickUp playerPickUp;

    [Header("Pause")]

    [SerializeField] private Transform pausePanel;
    [SerializeField] private Button mainMenuButton;

    private void Awake() 
    {
        mainMenuButton.onClick.AddListener(() => {
            SceneManager.LoadScene("SN_MainMenu");
        });  
    }

    private void Start() 
    {
        playerPickUp.OnStateChanged += PlayerPickUp_OnStateChanged;

        S_EventManager.Instance.OnToggleIsPaused += S_EventManager_OnToggleIsPaused;
    }

    private void S_EventManager_OnToggleIsPaused(object sender, EventArgs e)
    {
        pausePanel.gameObject.SetActive(S_EventManager.Instance.isPaused);
    }


    private void PlayerPickUp_OnStateChanged(object sender, S_PlayerPickUp.OnStateChangedEventArgs e)
    {
        if(e.state == S_PlayerPickUp.PlayerActionState.Interact)
        {
            ActiveInteractPanel();
        }
        else if(e.state == S_PlayerPickUp.PlayerActionState.Inspect)
        {
            ActiveInspectPanel();
        }
        else if(e.state == S_PlayerPickUp.PlayerActionState.AnyState)
        {
            DisablePanels();
        }
    }

    private void ActiveInteractPanel()
    {
        interactPanel.gameObject.SetActive(true);
        inspectPanel.gameObject.SetActive(false);

        //Liberar move
        //liberar arremesso
            //mouse esquerdo = drop
    }

    private void ActiveInspectPanel()
    {
        interactPanel.gameObject.SetActive(false);
        inspectPanel.gameObject.SetActive(true);

        //congelar move
        //trancar arremesso
            //mouse esquerdo = rotate
    }

    private void DisablePanels()
    {
        interactPanel.gameObject.SetActive(false);
        inspectPanel.gameObject.SetActive(false);
    }

}
