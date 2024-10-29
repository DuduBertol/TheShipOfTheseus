using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Chest : MonoBehaviour
{
    public bool isOpen;
    [SerializeField] private string password;
    [SerializeField] private string tempPassword;

    [SerializeField] private Transform chestTop;

    [SerializeField] private List<S_SingleSlider> slidersList;

    public void OpenChest()
    {
        Debug.Log("Baú Aberto!");
<<<<<<< Updated upstream

        Destroy(chestTop.gameObject);
    }
=======
    
        isOpen = true;
        animator.SetTrigger("OpenDoor");
        }
>>>>>>> Stashed changes

    public void CheckPassword()
    {   
        GetPassword();

        if(tempPassword == password)
        {
            Debug.Log("Senha Correta!");

            OpenChest();
        }
        else
        {
            Debug.Log("ERRO - Senha Incorreta!");
        }
    }

    private void GetPassword()
    {
        tempPassword = "";

        for (int i = 0; i < slidersList.Count; i++)
        {
            tempPassword += slidersList[i].activeValue.ToString();
        }
    }
}
