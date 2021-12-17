using UnityEngine;

public class Music : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);  //On game restart, if theres more than one music player, destroy this one

        DontDestroyOnLoad(this.gameObject); //Don't destroy the initial music player on game restart
    }
}
