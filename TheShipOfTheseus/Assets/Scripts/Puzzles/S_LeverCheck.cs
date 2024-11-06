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

    [SerializeField] private List<MeshRenderer> sunCables;
    [SerializeField] private List<MeshRenderer> moonCables;
    [SerializeField] private Material offMaterial;
    [SerializeField] private Material onSunMaterial;
    [SerializeField] private Material onMoonMaterial;

    private void Start() 
    {
        TurnOffCables();
    }

    private void TurnOnEnergy()
    {
        Debug.Log("Energia Ligada!");
        
        isEnergyOn = true;

        S_EventManager.Instance.Energy_SpawnLetter();
        S_EventManager.Instance.Energy_SpawnLoveKey();
        
        TurnOnCables();
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

    private void TurnOnCables()
    {
        // SUN
        for (int i = 0; i < sunCables.Count; i++)
        {
            sunCables[i].material = onSunMaterial;
        }   

        // MOON
        for (int i = 0; i < moonCables.Count; i++)
        {
            moonCables[i].material = onMoonMaterial;
        }   
    }
    private void TurnOffCables()
    {
        // SUN
        for (int i = 0; i < sunCables.Count; i++)
        {
            sunCables[i].material = offMaterial;
        }   

        // MOON
        for (int i = 0; i < moonCables.Count; i++)
        {
            moonCables[i].material = offMaterial;
        }   
    }

}
