using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public GameObject obstaclesEasy;
    public GameObject obstaclesHard;

    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager not found. Defaulting to EASY difficulty.");
            ApplyEasy();
            return;
        }

        if (GameManager.Instance.difficulty == DifficultyType.Easy)
        {
            ApplyEasy();
        }
        else
        {
            ApplyHard();
        }
    }

    void ApplyEasy()
    {
        Debug.Log("Difficulty applied: EASY");

        obstaclesEasy.SetActive(true);
        obstaclesHard.SetActive(false);
    }

    void ApplyHard()
    {
        Debug.Log("Difficulty applied: HARD");

        obstaclesEasy.SetActive(false);
        obstaclesHard.SetActive(true);
    }
}