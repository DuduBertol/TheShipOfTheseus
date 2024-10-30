using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlanetPlacer : MonoBehaviour
{
    public string placerPlanetName;
    public bool isCorrect;
    
    [SerializeField] private Transform planetPos;
    [SerializeField] private S_PlanetCheck planetCheck;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.TryGetComponent(out S_Planet planet))
        {
            Debug.Log("Achei um Planeta!");

            planet.canBePlaced = true;

            if(planet.transform.parent == null)
            {
                planet.SetParent(planetPos);

                if(planet.planetName == placerPlanetName)
                {
                    isCorrect = true;
                    planet.FreezePlanet();

                    Debug.Log("Planeta Correto!");

                    planetCheck.CheckAllPlanets();
                }
                else
                {
                    isCorrect = false;
                    Debug.Log("Planeta Errado!");
                }
            }
        }    
    }

    private void OnTriggerExit(Collider other) 
    {

        if(other.gameObject.TryGetComponent(out S_Planet planet))
        {
            Debug.Log("Saindo um Planeta!");

            planet.canBePlaced = false;
        }      
    }
}
