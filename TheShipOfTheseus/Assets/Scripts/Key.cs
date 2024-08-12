using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider) 
    {   
        if(collider.gameObject.CompareTag("FinalDoor"))
        {
            Destroy(collider.gameObject);
            GameController.Instance.PlayFinalEvent();
            
            SceneManager.LoadScene(2);
        }
    }

}
