using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [Header("References")]
    public Light directionalLight;

    [Header("Skyboxes")]
    public Material daySkybox;
    public Material nightSkybox;

    void Start()
    {
        // If GameManager does not exist (e.g., starting GameplayScene directly)
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager not found. Defaulting to DAY environment.");
            ApplyDefaultEnvironment();
            return;
        }

        ApplyEnvironment();
    }

    void ApplyEnvironment()
    {
        if (GameManager.Instance.environment == EnvironmentType.Day)
        {
            // DAY SETTINGS
            RenderSettings.skybox = daySkybox;

            directionalLight.intensity = 1.0f;
            directionalLight.color = Color.white;

            RenderSettings.fog = false;
        }
        else
        {
            // NIGHT SETTINGS
            RenderSettings.skybox = nightSkybox;

            directionalLight.intensity = 0.4f;
            directionalLight.color = new Color(0.6f, 0.7f, 1f);

            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.05f, 0.05f, 0.1f);
            RenderSettings.fogDensity = 0.02f;
        }
    }

    void ApplyDefaultEnvironment()
    {
        // Default fallback = Day
        RenderSettings.skybox = daySkybox;

        directionalLight.intensity = 1.0f;
        directionalLight.color = Color.white;

        RenderSettings.fog = false;
    }
}