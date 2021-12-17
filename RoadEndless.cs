using UnityEngine;

public class RoadEndless : MonoBehaviour
{
    RoadSpawn roadSpawn;
    public GameObject Obstacle;
    public GameObject Fuel;
    public GameObject Passenger;
    float[] RowIndex = new float[] { 0, 3.3f, -3.3f }; //Row position values
    float[] ObstacleRot = new float[] { 0, 180 }; //Meteor y axis rotation

    // Start is called before the first frame update
    void Start()
    {
        roadSpawn = GameObject.FindObjectOfType<RoadSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove.alive == false)
            CancelInvoke("DestroyTile"); //Dont destroy tile behind
    }

    private void OnTriggerExit(Collider other)
    {
        if(FuelAndUI.IsWarpActive == false)
            roadSpawn.TileSpawn(true); //If not warping, spawn normal tiles
        else if(FuelAndUI.IsWarpActive == true)
        {
            roadSpawn.TileSpawn(false); //If warping, spawn empty tiles
            FuelAndUI.WarpActiveRound = FuelAndUI.WarpActiveRound + 1; //Count each empty tile
        }

        Invoke("DestroyTile", 1); //Destroy obstacle behind character when moving
    }

    void DestroyTile()
    {
        Destroy(gameObject);
    }

    public void SpawnFuelEssence() //Fuel essence spawner
    {
        int FueltoSpawn = Random.Range(0, 2);
        for(int i=0; i<FueltoSpawn; i++)
        {
            GameObject temp = Instantiate(Fuel, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    public void SpawnPassenger() //Passenger spawner
    {
        int PassengerChance1 = Random.Range(0, 2);
        for (int j = 0; j < PassengerChance1; j++)
        {
                int PassengertoSpawn = Random.Range(0, 2);
                for (int i = 0; i < PassengertoSpawn; i++)
                {
                    GameObject temporary = Instantiate(Passenger, transform);
                    temporary.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
                }
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider) //Randomly picks a lane and position in the lane
    {
        int randomValue = Random.Range(0, RowIndex.Length);

        Vector3 point = new Vector3(RowIndex[randomValue], 1 ,Random.Range(collider.bounds.min.z, collider.bounds.max.z));

        if(point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        return point;
    }

    public void ObstacleSpawn() //Obstacle spawner
    {
        int YValue = Random.Range(0, ObstacleRot.Length); //Y axis rotation determination

        int SpawnIndex1 = Random.Range(2, 5); //Determines which row
        Transform SpawnPoint = transform.GetChild(SpawnIndex1).transform;
        Instantiate(Obstacle, SpawnPoint.position, Quaternion.Euler(Random.Range(0, 360), 0, ObstacleRot[YValue]), transform); //Spawns obstacle

        //int ObstacleChance = Random.Range(0, 2);
        //for (int i = 0; i < ObstacleChance; i++) //Spawn obstacle in the middle
        //{
        //    ZValue = Random.Range(0, ObstacleRot.Length);

        //    int SpawnIndex2 = Random.Range(5, 8);
        //    while (SpawnIndex2 - SpawnIndex1 == 3)
        //        SpawnIndex2 = Random.Range(5, 8);
        //    SpawnPoint = transform.GetChild(SpawnIndex2).transform;
        //    Instantiate(Obstacle, SpawnPoint.position, Quaternion.Euler(Random.Range(0, 360), 0, ObstacleRot[ZValue]), transform);
        //}

        int ObstacleChance = Random.Range(0, 2);
        for (int i = 0; i < ObstacleChance; i++) //Spawn on same row a 2nd obstacle
        {
            YValue = Random.Range(0, ObstacleRot.Length);

            int SpawnIndex2 = Random.Range(2, 5);
            while (SpawnIndex2 == SpawnIndex1)
                SpawnIndex2 = Random.Range(2, 5);
            SpawnPoint = transform.GetChild(SpawnIndex2).transform;
            Instantiate(Obstacle, SpawnPoint.position, Quaternion.Euler(Random.Range(0, 360), 0, ObstacleRot[YValue]), transform);
        }
    }
}
