using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenuController : MonoBehaviour
{
    [SerializeField] private Transform startText;
    [SerializeField] private Transform loadingImage;

    private bool isFirstInput = true;

    private void Update() 
    {
        if(Input.anyKeyDown && isFirstInput)
        {
            isFirstInput = false;

            loadingImage.gameObject.SetActive(true);
            startText.gameObject.SetActive(false);

            SceneManager.LoadScene("SN_Official");
        }    
    }
}
