using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartInput : MonoBehaviour
{
    [SerializeField] private Button quitGameButton;
    
    private void Awake() 
    {
        quitGameButton.onClick.AddListener(() => {
            Application.Quit();
        });  
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(1);
        }       
    }
}
