using UnityEngine;


public class SkyboxRotator : MonoBehaviour
{
    public float Speed = 1;

    protected void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * Speed);
    }
}