using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI time;
    public static float timeValue; //Determined in FuelAndUI script

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove.alive == true)
        {
            if (timeValue > 0) //Decreases time value over time
                timeValue = timeValue - Time.deltaTime;
            else timeValue = 0;

            DisplayTime(timeValue);
        }
    }

    void DisplayTime(float TimeLeftToDisplay)
    {
        if (TimeLeftToDisplay < 0) //If timer ran out, sets it inactive
        {
            TimeLeftToDisplay = 0;
            time.gameObject.SetActive(false);
        }

        float seconds = Mathf.FloorToInt(TimeLeftToDisplay % 60); //Counts seconds
        float milliseconds = TimeLeftToDisplay % 1 * 1000; //Counts miliseconds

        time.text = string.Format("{0:00}:{1:000}", seconds, milliseconds); //Outputs the time into text
    }
}
