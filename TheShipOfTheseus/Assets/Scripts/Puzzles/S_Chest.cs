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
    
        isOpen = true;
        animator.SetTrigger("OpenDoor");

        SoundManager.Instance.PlayLockerSound(transform.position, 0.4f);
        SoundManager.Instance.PlayOpenChestSound(transform.position, 0.4f);
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
