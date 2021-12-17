using UnityEngine;

public class SkyboxSpin : MonoBehaviour
{
    public float RotateSpeed;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);
    }
}
