using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject menuCanvas, HTPCanvas, Music;
    FuelAndUI fuelAndUI;

    private void Start()
    {
        fuelAndUI = GameObject.FindObjectOfType<FuelAndUI>();
    }

    public void PlayAgainOnClick()
    {
        PassengerAndUI.AstronautCount = 0;
        PassengerAndUI.DeliveredAstronautCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerMove.alive = true;
    }

    public void MainMenuOnClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void PlayOnClick()
    {
        PlayerMove.alive = true;
        SceneManager.LoadScene("MainScene");
    }

    public void ExitOnClick()
    {
        Application.Quit();
    }

    public void HowToPlayOnClick()
    {
        HTPCanvas.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(false);
    }

    public void BackButtonOnClick()
    {
        menuCanvas.gameObject.SetActive(true);
        HTPCanvas.gameObject.SetActive(false);
    }

    public void WarpDriveOnClick()
    {
        fuelAndUI.OnClick();
    }    
}
