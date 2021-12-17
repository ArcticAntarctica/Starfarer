using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    void Awake()
    {
        GameObject A = GameObject.FindGameObjectWithTag("Music"); //When going to menu from game, destroy the game music player
        Destroy(A);
    }
}
