using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_MainMenuController : MonoBehaviour
{
    [SerializeField] private Transform startText;
    [SerializeField] private Transform loadingImage;
    [SerializeField] private Button quitButton;

    private bool isFirstInput = true;

    private void Awake() 
    {
        quitButton.onClick.AddListener( () => {
            Application.Quit();
        });  
    }

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Confined;    
        Cursor.visible = true;    
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && isFirstInput)
        {
            isFirstInput = false;

            loadingImage.gameObject.SetActive(true);
            startText.gameObject.SetActive(false);

            SceneManager.LoadScene("SN_Official");
        }    
    }
}
