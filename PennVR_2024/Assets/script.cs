using UnityEngine;

public class ShowerFogController : MonoBehaviour
{
    public ParticleSystem fogParticleSystem; // Assign your fog Particle System
    public float fadeDuration = 3.0f; // Duration of fade in/out

    private float targetEmissionRate = 0f;
    private float currentEmissionRate = 0f;

    private bool isShowerOn = false;

    void Start()
    {
        // Start with no fog
        var emissionModule = fogParticleSystem.emission;
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
    }

    void Update()
    {
        // Toggle shower with "E" key (example input)
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleShower();
        }

        // Smoothly interpolate the emission rate
        currentEmissionRate = Mathf.Lerp(currentEmissionRate, targetEmissionRate, Time.deltaTime / fadeDuration);
        var emissionModule = fogParticleSystem.emission; // Get a fresh copy
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(currentEmissionRate);
    }

    void ToggleShower()
    {
        isShowerOn = !isShowerOn;

        if (isShowerOn)
        {
            targetEmissionRate = 50f; // Fog starts to fade in
            fogParticleSystem.Play();
            Debug.Log("Shower turned ON - Fog fades in.");
        }
        else
        {
            targetEmissionRate = 0f; // Fog starts to fade out
            Debug.Log("Shower turned OFF - Fog fades out.");
        }
    }
}
