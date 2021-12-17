using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class FuelAndUI : MonoBehaviour
{
    public static bool IsWarpActive = false; //Connected to tile spawn (checks when to keep spawning empty tiles)
    public static bool IsWarpEffectActive = false; //Connected to player speed (checks when to speed player up)
    bool IsFuelFull = false; //Checks if fuel capacity is full
    bool PlanetaryStatus; //Early bool check for coroutine
    public static int WarpActiveRound = 0; //Empty tile count
    float FuelCapScore = 0; //Fuel percentage gathered
    float FuelBarScore = 10; //Fuel bar meter fill up interval
    public Texture Empty, Full;
    public ParticleSystem Warp1, Warp2, Stars; 
    private ParticleSystem StarParticle;
    public RawImage FuelPoint1, FuelPoint2, FuelPoint3, FuelPoint4, FuelPoint5, FuelPoint6, FuelPoint7, FuelPoint8, FuelPoint9, FuelPoint10;
    public Button FuelButton;
    public TextMeshProUGUI FuelCap, FuelButtonText, TimerCountdown;
    SoundEffects soundEffects;
    PlanetGeneration planetGeneration;

    // Start is called before the first frame update
    void Start()
    {
        StarParticle = Stars.GetComponent<ParticleSystem>();
        soundEffects = GameObject.FindObjectOfType<SoundEffects>();
        planetGeneration = GameObject.FindObjectOfType<PlanetGeneration>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fuel")
        {
            soundEffects.PlayFuelPickUp();
            Destroy(other.gameObject);
            if (FuelCapScore != 100)
            {
                FuelCapScore += 10; // Originally the value is 5
                FuelCap.text = FuelCapScore + "%";
            }

            if(FuelCapScore == FuelBarScore) //Fill the fuel bar for every 10%
            {
                if (FuelBarScore == 10)
                    FuelPoint1.texture = Full;
                else if (FuelBarScore == 20)
                    FuelPoint2.texture = Full;
                else if (FuelBarScore == 30)
                    FuelPoint3.texture = Full;
                else if (FuelBarScore == 40)
                    FuelPoint4.texture = Full;
                else if (FuelBarScore == 50)
                    FuelPoint5.texture = Full;
                else if (FuelBarScore == 60)
                    FuelPoint6.texture = Full;
                else if (FuelBarScore == 70)
                    FuelPoint7.texture = Full;
                else if (FuelBarScore == 80)
                    FuelPoint8.texture = Full;
                else if (FuelBarScore == 90)
                    FuelPoint9.texture = Full;
                else if (FuelBarScore == 100)
                {
                    FuelPoint10.texture = Full;
                    FuelButton.gameObject.SetActive(true);
                    IsFuelFull = true;
                    StartCoroutine("FlashText");
                }

                    FuelBarScore = FuelBarScore + 10;
            }
        }
    }

    IEnumerator FlashText()
    {
        while(IsFuelFull == true)
        {
            FuelButtonText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);

            FuelButtonText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator WarpEffect()
    {
            yield return new WaitForSeconds(5f);
            IsWarpEffectActive = true; //Player speed speed up
            var main = StarParticle.main;
            main.simulationSpeed = 0.58f; //Speed the stars up
            CameraShake.Shake = true; //Activate camera shake
            Warp1.GetComponent<ParticleSystem>().Play(); //Play warp drive effect
            Warp2.GetComponent<ParticleSystem>().Play();

            yield return new WaitForSeconds(.5f);
            if (PlanetaryStatus)
                planetGeneration.PlanetaryFlyOut(); //If in civilization zone, on warp, fly away from the planet + moons

            yield return new WaitForSeconds(5.5f);
            planetGeneration.PlanetAndMoonDespawn(); //Remove planet + moons if any exist

            if (!PlanetaryStatus)  //If in open space, on warp, fly in on the planet + moons
            {
                planetGeneration.PlanetAndMoonSpawn();
                planetGeneration.PlanetaryFlyIn();
            }
            Warp1.GetComponent<ParticleSystem>().Stop(); //Stop warp drive effect
            Warp2.GetComponent<ParticleSystem>().Stop();

            yield return new WaitForSeconds(.5f);
            IsWarpEffectActive = false; //Player speed slow down

            yield return new WaitForSeconds(1f);
            main.simulationSpeed = 0.1f; //Slow down the stars
            FuelCapScore = 0; //Fuel percentage back to 0% after warp
            FuelBarScore = 10; //Fuel bar minimum limit back to 10
            FuelCap.text = FuelCapScore + "%";
            FuelPoint1.texture = Empty;
            FuelPoint2.texture = Empty;
            FuelPoint3.texture = Empty;
            FuelPoint4.texture = Empty;
            FuelPoint5.texture = Empty;
            FuelPoint6.texture = Empty;
            FuelPoint7.texture = Empty;
            FuelPoint8.texture = Empty;
            FuelPoint9.texture = Empty;
            FuelPoint10.texture = Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove.alive == false) //If player dies, to stop warp
        {
            StopCoroutine("WarpEffect");
            soundEffects.StopWarpTimer();
            IsWarpActive = false;
            WarpActiveRound = 0; 
        }
        else
        {
            if (WarpActiveRound == 16) //Empty tile counter
            {
                IsWarpActive = false; //Stop spawning empty tiles
                WarpActiveRound = 0;
            }
        }
    }

    public void OnClick() //When warp button is clicked
    {
        if (PlanetGeneration.IsHabitable == false)
        {
            PlanetaryStatus = false; //Early check for coroutine
            RoadSpawn.EarlySpawnCheck = true; //Early check for passenger spawn on road
        }
        else if (PlanetGeneration.IsHabitable == true)
        {
            PlanetaryStatus = true; //Early check for coroutine
            RoadSpawn.EarlySpawnCheck = false; //Early check for passenger spawn on road
        }

        IsFuelFull = false; //Stops button flash coroutine
        FuelButton.gameObject.SetActive(false); //Deactivate button
        soundEffects.PlayWarpTimer();
        Timer.timeValue = 5f; //Timer countdown interval
        IsWarpActive = true; //Empty tile spawn activation
        StartCoroutine("WarpEffect");
        TimerCountdown.gameObject.SetActive(true); //Starts the timer
    }
}
