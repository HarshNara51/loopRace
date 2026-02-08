using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Environment selection
    public void SelectDay()
    {
        GameManager.Instance.environment = EnvironmentType.Day;
        Debug.Log("Environment set to DAY");
    }

    public void SelectNight()
    {
        GameManager.Instance.environment = EnvironmentType.Night;
        Debug.Log("Environment set to NIGHT");
    }

    // Difficulty selection
    public void SelectEasy()
    {
        GameManager.Instance.difficulty = DifficultyType.Easy;
        Debug.Log("Difficulty set to EASY");
    }

    public void SelectHard()
    {
        GameManager.Instance.difficulty = DifficultyType.Hard;
        Debug.Log("Difficulty set to HARD");
    }

    // Start game
    public void StartGame()
    {
        Debug.Log("Starting game...");
        SceneManager.LoadScene("GameplayScene");
    }
}