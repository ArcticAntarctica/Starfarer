using UnityEngine;

public class WarpFollow : MonoBehaviour
{
    public Transform PlayerCharacter;
    Vector3 WarpDistance;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Stop();
        WarpDistance = transform.position - PlayerCharacter.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 WarpPosition = PlayerCharacter.position + WarpDistance;
        WarpPosition.x = -0.3f;

        transform.position = WarpPosition;
    }
}
