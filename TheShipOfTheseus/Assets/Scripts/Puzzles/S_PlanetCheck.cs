using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlanetCheck : MonoBehaviour
{
    [SerializeField] private int passwordCounter;
    [SerializeField] private int tempPasswordCounter;
    [SerializeField] private List<S_PlanetPlacer> planetPlacers;
    
    public void CheckAllPlanets()
    {
        for (int i = 0; i < planetPlacers.Count; i++)
        {
            if(planetPlacers[i].isCorrect)
            {
                tempPasswordCounter++;
            }
        }

        if(passwordCounter == tempPasswordCounter)
        {
            S_EventManager.Instance.Planets_SpawnMOONKey();
            S_EventManager.Instance.Planets_SpawnSUNKey();
        }
        else
        {
            tempPasswordCounter = 0;
        }
    }
}
