using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float ShakeStrength = 0.1f;
    float ShakeDuration = 7;
    float ShakeSlowDown = 1;
    public static bool Shake = false; //Checks when to shake
    public Transform Cam;
    Vector3 ShakePos;
    float InitialDuration;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main.transform;
        ShakePos = Cam.localPosition;
        InitialDuration = ShakeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(Shake)
            if(ShakeDuration > 0)
            {
                Cam.localPosition = ShakePos + Random.insideUnitSphere * ShakeStrength;
                ShakeDuration -= Time.deltaTime * ShakeSlowDown;
            }
            else
            {
                Shake = false;
                ShakeDuration = InitialDuration;
                Cam.localPosition = ShakePos;
            }
    }
}
