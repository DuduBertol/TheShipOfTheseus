using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SecretBook : MonoBehaviour
{

    public string activeValue;
    [SerializeField] private string charSymbol;

    [SerializeField] private S_BookshelfSecretDoor bookshelfSecretDoor;

    private bool isActive;
    private Vector3 startPos;

    private void Start() 
    {
        bookshelfSecretDoor.OnFailed += BookshelfSecretDoor_OnFailed;

        startPos = transform.localPosition;
    }

    private void BookshelfSecretDoor_OnFailed(object sender, EventArgs e)
    {
        ResetPos();
    }

    public void Interact()
    {
        if(!bookshelfSecretDoor.isOpen && !isActive)
        {
            isActive = true;
            
            InteractPos();

            SoundManager.Instance.PlayGearSound(transform.position, 0.2f);

            activeValue = charSymbol;
            bookshelfSecretDoor.AddToPassword(activeValue);
        }
    }

    private void ResetPos()
    {
        transform.localPosition = startPos;
        isActive = false;
    }

    private void InteractPos()
    {
        float distaceToMove = 0.2f;

        transform.localPosition += new Vector3 (distaceToMove, 0, 0);
    }

    
}