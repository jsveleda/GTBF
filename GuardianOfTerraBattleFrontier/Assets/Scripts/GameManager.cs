using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public GameObject targetPrefab;
    private int numberOfTargets = 10;

    private int score = 0;
    public int enemiesLeft;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        score = 0;
        UpdateScoreUI();

        SpawnTargets(numberOfTargets);

        enemiesLeft = numberOfTargets;
    }

    private void SpawnTargets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(targetPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-20f, 20f);
        return new Vector3(x, y, 0f);
    }

    public void IncrementScore(int points)
    {
        score += points;
        enemiesLeft--;

        UpdateScoreUI();

        if (enemiesLeft == 0)
        {
            enemiesLeft = numberOfTargets;
            SpawnTargets(numberOfTargets);
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
