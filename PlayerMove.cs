using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;
    float speed1 = 11; //Normal sleep
    float speed2 = 20; //Warp speed
    public static bool alive = true;
    int location = 2; // On which row is the character, counting from left
    float PositionValue = 3.3f; // One row jump interval
    float NewPosition = 0; // Each position change
    bool MoveReadLeft = false;
    bool MoveReadRight = false;
    Vector3 MoveForward;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) //Reads key inputs
            MoveReadLeft = true;
        if (Input.GetKeyDown(KeyCode.D))
            MoveReadRight = true;
    }

    private void FixedUpdate()
    {
        if (!alive)
            return;

        if(FuelAndUI.IsWarpEffectActive == false)
            MoveForward = transform.forward * speed1 * Time.fixedDeltaTime; //If not warping, normal speed

        if (FuelAndUI.IsWarpEffectActive == true)
            MoveForward = transform.forward * speed2 * Time.fixedDeltaTime; //If warping, speed up

        if (MoveReadLeft == true)
            if (location != 1) //Jump to left lane
            {
                NewPosition -= PositionValue;
                location = location - 1;
                MoveReadLeft = false;
            }
            else if (location == 1) //If already in the most left lane, do not jump
                MoveReadLeft = false;

        if (MoveReadRight == true) //Jump to right lane
            if (location != 3)
            {
                NewPosition += PositionValue;
                location = location + 1;
                MoveReadRight = false;
            }
            else if (location == 3) //If already in the most right lane, do not jump
                MoveReadRight = false;

        Vector3 MoveHorizontal = (NewPosition - transform.position.x) * Vector3.right;
        rb.MovePosition(rb.position + MoveForward + MoveHorizontal); //Moves character forward
    }
}
