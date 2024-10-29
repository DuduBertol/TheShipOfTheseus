using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Lever : MonoBehaviour
{
    public int state;
    
    [SerializeField] private List<int> anglesList;
    [SerializeField] private Transform leverArm;

    [SerializeField] private S_LeverCheck leverCheck;


    public void Interact()
    {
        if(!leverCheck.isEnergyOn)
        {
            if(state < anglesList.Count-1)
            {
                state ++;
            }
            else
            {
                state = 0;
            }

            AngleSet();

            leverCheck.CheckPassword();
        }
    }

    private void AngleSet()
    {
        leverArm.rotation = Quaternion.Euler(0, 0, anglesList[state]);
    }
}
