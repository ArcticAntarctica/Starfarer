using UnityEngine;

public class StarsFollow : MonoBehaviour
{
    public Transform PlayerCharacter;
    Vector3 StarsDistance;

    // Start is called before the first frame update
    void Start()
    {
        StarsDistance = transform.position - PlayerCharacter.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 StarsPosition = PlayerCharacter.position + StarsDistance;
        StarsPosition.x = -0.3f;

        transform.position = StarsPosition;
    }
}
