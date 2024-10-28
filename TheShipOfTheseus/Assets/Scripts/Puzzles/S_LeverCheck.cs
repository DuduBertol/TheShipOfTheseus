using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LeverCheck : MonoBehaviour
{
    public bool isEnergyOn;
    
    [Header("MOON = 1 | SUN = 3")]
    [SerializeField] private string password;
    [SerializeField] private string tempPassword;

    [SerializeField] private List<S_Lever> leversList;

    private void TurnOnEnergy()
    {
        Debug.Log("Energia Ligada!");
        
        isEnergyOn = true;
        
        //Spawn Primeira Carta
    }

    public bool CheckPassword()
    {   
        GetPassword();

        if(tempPassword == password)
        {
            Debug.Log("Senha Correta!");
            
            TurnOnEnergy();

            return true;
        }
        else
        {
            return false;
        }
    }

    private void GetPassword()
    {
        tempPassword = "";

        for (int i = 0; i < leversList.Count; i++)
        {
            tempPassword += leversList[i].state.ToString();
        }
    }



}
