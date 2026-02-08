using UnityEngine;

public enum EnvironmentType { Day, Night }
public enum DifficultyType { Easy, Hard }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnvironmentType environment;
    public DifficultyType difficulty;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}