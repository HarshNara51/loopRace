using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [Header("Lighting Groups")]
    public GameObject dayLightingRoot;
    public GameObject nightLightingRoot;

    [Header("Skyboxes")]
    public Material daySkybox;
    public Material nightSkybox;

    [Header("Ambient Colors")]
    public Color dayAmbient;
    public Color nightAmbient;

    void Start()
    {
        ApplyEnvironment();
    }

    void ApplyEnvironment()
    {
        if (GameManager.Instance.environment == EnvironmentType.Night)
            SetNight();
        else
            SetDay();
    }

    void SetDay()
    {
        dayLightingRoot.SetActive(true);
        nightLightingRoot.SetActive(false);

        RenderSettings.skybox = daySkybox;
        RenderSettings.ambientLight = dayAmbient;
        RenderSettings.fog = false;

        DynamicGI.UpdateEnvironment();
    }

    void SetNight()
    {
        dayLightingRoot.SetActive(false);
        nightLightingRoot.SetActive(true);

        RenderSettings.skybox = nightSkybox;
        RenderSettings.ambientLight = nightAmbient;
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.1f, 0.1f, 0.2f);

        DynamicGI.UpdateEnvironment();
    }
}