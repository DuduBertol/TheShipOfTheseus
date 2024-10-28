using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Chest : MonoBehaviour
{
    [SerializeField] private string password;
    [SerializeField] private string tempPassword;

    [SerializeField] private Animator animator;
    [SerializeField] private List<S_SingleSlider> slidersList;

    private void OpenChest()
    {
        Debug.Log("Baú Aberto!");
        animator.SetTrigger("OpenDoor");
    }

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
