using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerCharacter;
    Vector3 CamDistance;

    // Start is called before the first frame update
    void Start()
    {
        CamDistance = transform.position - PlayerCharacter.position; //Find the distance between camera and player
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CamPosition = PlayerCharacter.position + CamDistance; //Apply the distance
        CamPosition.x = 7.26f;
        CamPosition.y = 10.35f;
        
        transform.position = CamPosition;
    }
}
