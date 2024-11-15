using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Lever : MonoBehaviour
{
    public int state;
    
    [SerializeField] private List<int> anglesList;
    [SerializeField] private Transform leverArm;

    [SerializeField] private S_LeverCheck leverCheck;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSystem particle2;


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

            SoundManager.Instance.PlayLeverSound(transform.position, 0.3f);
            particle.Play();
            particle2.Play();
            
            leverCheck.CheckPassword();
        }
    }

    private void AngleSet()
    {
        leverArm.rotation = Quaternion.Euler(0, 0, anglesList[state]);

    }
}
