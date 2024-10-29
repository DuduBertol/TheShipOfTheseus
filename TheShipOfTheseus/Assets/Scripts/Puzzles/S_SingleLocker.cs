using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_SingleLocker : MonoBehaviour
{

    public string activeValue;

    [SerializeField] private int index;
    [SerializeField] private List<string> charList;
    [SerializeField] private List<TextMeshProUGUI> lockerTextList;
    [SerializeField] private Transform lockerArm;
    
    [SerializeField] private S_Locker lockerParent;

    private void Start() 
    {
        StarterVisual();
        UpdateValue();
    }

    public void Interact()
    {
        if(!lockerParent.isOpen)
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

            lockerParent.CheckPassword();
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
