using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_SingleVaultLocker : MonoBehaviour
{

    public string activeValue;

    [SerializeField] private int index;
    [SerializeField] private List<string> charList;
    [SerializeField] private List<TextMeshProUGUI> lockerTextList;
    [SerializeField] private Transform lockerArm;
    
    [SerializeField] private S_Vault vaultParent;

    private void Start() 
    {
        StarterVisual();
        UpdateValue();
    }

    public void Interact()
    {
        if(!vaultParent.isOpen)
        {
            if(index < charList.Count-1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            
            lockerArm.Rotate(0, 60, 0);
            UpdateValue();

            SoundManager.Instance.PlayGearSound(transform.position, 0.2f);

            vaultParent.CheckPassword();
        }
    }

    private void UpdateValue()
    {
        activeValue = charList[index];
    }

    private void StarterVisual()
    {
        for (int i = 0; i < charList.Count; i++)
        {
            lockerTextList[i].text = charList[i].ToString();
        }
    }

    
}
