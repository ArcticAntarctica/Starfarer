using System.Collections;
using TMPro;
using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    public Canvas CanvasGameOver;
    public ParticleSystem Stars;
    public TextMeshProUGUI PassengerCount1, PassengerCount2, TotalScoreCount;
    int TotalScore = 0; //Total game score counter
    public ParticleSystem Explosion;
    SoundEffects soundEffects;

    private void Start()
    {
        soundEffects = GameObject.FindObjectOfType<SoundEffects>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
            Death();
    }

    public void Death() //Player death on obstacle hit
    {
        PlayerMove.alive = false;
        PlanetGeneration.IsHabitable = false; //Resets static
        RoadSpawn.EarlySpawnCheck = false; //Resets static
        Stars.GetComponent<ParticleSystem>().Pause(); //Stop star movement
        Explosion.GetComponent<ParticleSystem>().Play(); //Play explosion effect
        soundEffects.PlayCrash();
        StartCoroutine("PassengerCounter2"); //Game over screen score counter coroutine
    }

    IEnumerator PassengerCounter2()
    {
        TotalScore = 0;
        yield return new WaitForSeconds(.4f);
        CanvasGameOver.gameObject.SetActive(true);

        for (int i = 0; i <= PassengerAndUI.DeliveredAstronautCount; i++) //Counts delivered astronaut sum
        {
            PassengerCount1.text = i + "";
            yield return new WaitForSeconds(.3f);
        }
        
        for (int i = 0; i <= PassengerAndUI.AstronautCount; i++) //Counts on board astronaut sum
        {
            PassengerCount2.text = i + "";
            yield return new WaitForSeconds(.3f);
        }

        for(int i = 1; i <= PassengerAndUI.DeliveredAstronautCount; i ++) //Delivered astronauts added to total score
        {
            TotalScore = TotalScore + 10;
            TotalScoreCount.text = TotalScore + "";
            yield return new WaitForSeconds(.3f);
        }

        for(int i = 1; i <= PassengerAndUI.AstronautCount; i ++) //On board astronauts added to total score
        {
            TotalScore = TotalScore + 5;
            TotalScoreCount.text = TotalScore + "";
            yield return new WaitForSeconds(.3f);
        }
    }
}
