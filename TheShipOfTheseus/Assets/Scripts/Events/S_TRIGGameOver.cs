using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TRIGGameOver : MonoBehaviour
{
    private S_PlayerCutscene playerCutscene;

    private void Start() 
    {
        playerCutscene = FindObjectOfType<S_PlayerCutscene>();    
    }

    private void OnCollisionEnter(Collision other) 
    {
        playerCutscene.GameOverCutscene();    

        S_PlayerMovement.Instance.isFreezed = true;
    }
}
