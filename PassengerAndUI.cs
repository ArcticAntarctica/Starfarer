using System.Collections;
using UnityEngine;
using TMPro;

public class PassengerAndUI : MonoBehaviour
{
    public TextMeshProUGUI AstronautText;
    public static int DeliveredAstronautCount = 0;
    public static int AstronautCount = 0;
    SoundEffects soundEffects;

    private void Start()
    {
        soundEffects = GameObject.FindObjectOfType<SoundEffects>();
    }
    private void Update()
    {
        if (!PlayerMove.alive)
           StopCoroutine("TotalAstronautCounter");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Passenger") //Picking up passengers counter
        {
            soundEffects.PlayPassengerPickUp();
            AstronautCount = AstronautCount + 1;
            AstronautText.text = AstronautCount + "";
            Destroy(other.gameObject);
        }
    }

    public IEnumerator TotalAstronautCounter() //Releases and counts astronauts in civilization zone
    {
        if (AstronautCount != 0)
        {
            yield return new WaitForSeconds(3);
            for (int i = AstronautCount; i > 0; i--)
            {
                DeliveredAstronautCount = DeliveredAstronautCount + 1;
                soundEffects.PlayPoint();
                AstronautCount = AstronautCount - 1;
                AstronautText.text = AstronautCount + "";
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
