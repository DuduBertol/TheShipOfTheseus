using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_SingleSlider : MonoBehaviour
{

    public string activeValue;

    [SerializeField] private int index;
    [SerializeField] private List<string> valuesList;
    [SerializeField] private List<TextMeshProUGUI> sliderTextList;
    
    [SerializeField] private S_Chest chestParent;

    private void Start() 
    {
        StarterVisual();
        UpdateValue();
    }

    public void Interact()
    {
        if(!chestParent.isOpen)
        {
            if(index < valuesList.Count-1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            
            transform.Rotate(0, 0, -60);
            UpdateValue();

            SoundManager.Instance.PlayGearSound(transform.position, 0.05f);

            chestParent.CheckPassword();
        }
    }

    private void UpdateValue()
    {
        activeValue = valuesList[index];
    }

    private void StarterVisual()
    {
        for (int i = 0; i < valuesList.Count; i++)
        {
            sliderTextList[i].text = valuesList[i].ToString();
        }
    }

    
}
