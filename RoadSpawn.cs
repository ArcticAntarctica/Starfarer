using UnityEngine;

public class RoadSpawn : MonoBehaviour
{
    public GameObject RoadTile;
    public static bool EarlySpawnCheck;
    Vector3 SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < 5; i++)
        {
            if (i < 2) TileSpawn(false); //First 2 tiles spawned without obstacles at the beginning of the game
            else TileSpawn(true); //Spawn 3 more tiles with obstacles
        }
    }

    public void TileSpawn(bool spawnObjects) //Tile spawner
    {
        GameObject TempTile = Instantiate(RoadTile, SpawnPoint, Quaternion.identity);
        SpawnPoint = TempTile.transform.GetChild(1).transform.position;

        if(spawnObjects) //Sees wether spawn empty tiles or regular tiles
        {
            TempTile.GetComponent<RoadEndless>().ObstacleSpawn();
            TempTile.GetComponent<RoadEndless>().SpawnFuelEssence();
            if(!EarlySpawnCheck)
            TempTile.GetComponent<RoadEndless>().SpawnPassenger();
        }
    }
}
