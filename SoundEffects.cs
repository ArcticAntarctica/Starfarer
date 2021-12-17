using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource Crash;
    public AudioSource FuelPickUp;
    public AudioSource WarpTimer;
    public AudioSource Point;
    public AudioSource PassengerPickUp;

    public void PlayCrash()
    {
        Crash.Play();
    }

    public void PlayFuelPickUp()
    {
        FuelPickUp.Play();
    }

    public void PlayWarpTimer()
    {
        WarpTimer.Play();
    }

    public void StopWarpTimer()
    {
        WarpTimer.Stop();
    }

    public void PlayPoint()
    {
        Point.Play();
    }

    public void PlayPassengerPickUp()
    {
        PassengerPickUp.Play();
    }
}
