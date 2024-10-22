using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    private void Update() 
    {
        //FREEZE CAM
        if(Input.GetKeyDown(KeyCode.F))
        {
            S_PlayerCam.Instance.isFreezed = !S_PlayerCam.Instance.isFreezed;
        }    
        
        //FREEZE MOVE
        if(Input.GetKeyDown(KeyCode.M))
        {
            S_PlayerMovement.Instance.isFreezed = !S_PlayerMovement.Instance.isFreezed;
        }  

        
    }
}
