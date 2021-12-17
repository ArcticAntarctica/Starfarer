using System.Collections.Generic;
using UnityEngine;

public class PlanetGeneration : MonoBehaviour
{
    public static bool IsHabitable = false;
    public GameObject PlanetPos;
    int PlanetBefore = 0; //Checks to not spawn same planet as before
    public List<GameObject> PlanetList;
    public List<GameObject> MoonList;
    public List<GameObject> SpawnPointList;
    PassengerAndUI passengerAndUI;

    private void Start()
    {
        passengerAndUI = GameObject.FindObjectOfType<PassengerAndUI>();
    }

    public void PlanetAndMoonSpawn()
    {
        int PlanetPrefab = Random.Range(0, PlanetList.Count); //Picks planet model
        while(PlanetPrefab == PlanetBefore)
            PlanetPrefab = Random.Range(0, PlanetList.Count);
        GameObject PlanetPoint = Instantiate(PlanetList[PlanetPrefab]); //Spawns planet
        PlanetPoint.transform.parent = gameObject.transform;
        PlanetPoint.transform.position = PlanetPos.transform.position;

        PlanetBefore = PlanetPrefab;

        int MoonSpawnRate, MoonPrefab;
        GameObject MoonPoint;

        for (int i = 0; i < SpawnPointList.Count; i++) //Runs through all spawn points
        {
            MoonSpawnRate = Random.Range(0, 2);
            for (int j = 0; j < MoonSpawnRate; j++) //Randomly picks the amount of moons
            {
                MoonPrefab = Random.Range(0, MoonList.Count); //Picks moon model
                MoonPoint = Instantiate(MoonList[MoonPrefab]); //Spawns moon
                MoonPoint.transform.parent = gameObject.transform;
                MoonPoint.transform.position = SpawnPointList[i].transform.position;
                if(SpawnPointList[i].name.Contains("MoonPos1L") || SpawnPointList[i].name.Contains("MoonPos2L") || SpawnPointList[i].name.Contains("MoonPos3L"))
                   MoonPoint.transform.localScale = new Vector3(100, 100, 100); //Moon size according to spawn point
                else if(SpawnPointList[i].name.Contains("MoonPos4M"))
                   MoonPoint.transform.localScale = new Vector3(50, 50, 50);
                else if(SpawnPointList[i].name.Contains("MoonPos5S"))
                   MoonPoint.transform.localScale = new Vector3(25, 25, 25);
            }
        }

        IsHabitable = true;
        StartCoroutine(passengerAndUI.TotalAstronautCounter()); //Starts passenger let out coroutine
    }

    public void PlanetAndMoonDespawn() //Despawns all moons and planet
    {
        foreach (Transform child in transform)
        {
            GameObject[] Planetaries = GameObject.FindGameObjectsWithTag("Planetary");
            foreach(GameObject Planetary in Planetaries)
            GameObject.Destroy(Planetary);
        }

        Animator Idle = GetComponent<Animator>();
        Idle.Play("Idle", 0, 0.0f);  //Resets animator to idle
        IsHabitable = false;
    }

    public void PlanetaryFlyIn()
    {
        Animator FlyIn = GetComponent<Animator>();
        FlyIn.Play("PlanetaryAnimation", 0, 0.0f);  //Sets animator to fly in animation
    }

    public void PlanetaryFlyOut()
    {
        Animator FlyOut = GetComponent<Animator>();
        FlyOut.Play("PlanetaryAnimationOut", 0, 0.0f); //Sets animator to fly out animation
    }
}
