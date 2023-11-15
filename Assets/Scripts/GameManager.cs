using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Player player;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI hpLabel;
    [SerializeField] private GameObject enemyPrefab;
    
    [SerializeField] private int numberOfEnemies = 10;

    public int Score { get; set; }
    public int EnemiesLeft { get; set; }

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
        Score = 0;
        UpdateUI();

        SpawnTargets(numberOfEnemies);

        EnemiesLeft = numberOfEnemies;
    }

    private void SpawnTargets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
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
        Score += points;
        EnemiesLeft--;

        UpdateUI();

        if (EnemiesLeft == 0)
        {
            EnemiesLeft = numberOfEnemies;
            SpawnTargets(numberOfEnemies);
        }
    }

    private void UpdateUI()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + Score;
        }

        if(hpLabel != null)
        {
            hpLabel.text = "Health Points: " + player.GetHealthPoints();
        }
    }

    internal void EnemyHitPlayer(int enemyDamage)
    {
        player.TakeDamage(enemyDamage);
    }

    internal Vector3 GetPlayerPosition()
    {
        return playerTransform.position;
    }
}
