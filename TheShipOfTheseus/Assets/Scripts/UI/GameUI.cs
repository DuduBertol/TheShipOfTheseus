using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Transform interactPanel;
    [SerializeField] private Transform inspectPanel;


    [SerializeField] private S_PlayerPickUp playerPickUp;


    private void Start() 
    {
        playerPickUp.OnStateChanged += PlayerPickUp_OnStateChanged;
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
