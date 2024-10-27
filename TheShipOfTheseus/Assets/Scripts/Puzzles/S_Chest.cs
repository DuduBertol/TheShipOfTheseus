using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Chest : MonoBehaviour
{
    [SerializeField] private string password;
    [SerializeField] private string tempPassword;

    [SerializeField] private Transform chestTop;

    [SerializeField] private List<S_SingleSlider> slidersList;

    public void OpenChest()
    {
        Debug.Log("Baú Aberto!");

        Destroy(chestTop.gameObject);
    }

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
