using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_PlayerUI : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject selectionCursor;
    [SerializeField] private Image dropCheck;
    
    [Header("Sprites")]
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite wrongSprite;

    [Header("Colors")]
    [SerializeField] private Color invisibleColor;
    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;

    [Header("References")]
    [SerializeField] private S_PlayerPickUp playerPickUp;

    private void Update() 
    {
        DropChecking();    
    }

    private void DropChecking()
    {
        S_PlayerPickUp.ObjectViewState state =  playerPickUp.GetObjectViewState();
        
        if (state == S_PlayerPickUp.ObjectViewState.View_Hold)
        {
            CorrectDropCheck();
        }
        else if (state == S_PlayerPickUp.ObjectViewState.NoView_Hold)
        {
            WrongDropCheck();
        }
        else if (state == S_PlayerPickUp.ObjectViewState.View_NoHold)
        {
            ClearDropCheck();
            ToggleSelectCursor(true);
        }
        else if (state == S_PlayerPickUp.ObjectViewState.NoView_NoHold)
        {
            ClearDropCheck();
            ToggleSelectCursor(false);
        }
    }

    private void CorrectDropCheck()
    {
        dropCheck.color = correctColor;
        dropCheck.sprite = correctSprite;
    }
    private void WrongDropCheck()
    {
        dropCheck.color = wrongColor;
        dropCheck.sprite = wrongSprite;
    }
    private void ClearDropCheck()
    {
        dropCheck.color = invisibleColor;
        dropCheck.sprite = null;
    }

    private void ToggleSelectCursor(bool value)
    {
        selectionCursor.SetActive(value);
    }
        

}
