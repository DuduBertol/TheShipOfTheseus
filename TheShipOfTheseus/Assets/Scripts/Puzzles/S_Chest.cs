using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Chest : MonoBehaviour
{
    public bool isOpen;
    [SerializeField] private string password;
    [SerializeField] private string tempPassword;

    [SerializeField] private Animator animator;
    [SerializeField] private List<S_SingleSlider> slidersList;

    private void OpenChest()
    {
        Debug.Log("Baú Aberto!");
<<<<<<< HEAD
<<<<<<< Updated upstream

        Destroy(chestTop.gameObject);
=======
        animator.SetTrigger("OpenDoor");
>>>>>>> d5c657cd1b61de5981f5aafbaa86067b3389d56f
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
