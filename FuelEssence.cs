using UnityEngine;

public class FuelEssence : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Passenger")
            Destroy(gameObject); //Destroy fuel essence orb if it spawns inside obstacle or passenger
    }

}
