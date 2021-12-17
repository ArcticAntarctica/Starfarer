using UnityEngine;

public class Passenger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Fuel")
            Destroy(gameObject); //If passenger spawns in obstacle or fuel orb, destroy it
    }
}
