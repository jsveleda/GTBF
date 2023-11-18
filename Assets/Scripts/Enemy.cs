using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int damage = 1;
    [SerializeField] private int scoreValue = 2;

    [HideInInspector] public UnityEvent<int> OnEnemyHit;
    [HideInInspector] public UnityEvent<int> OnEnemyDeath;

    private void Update()
    {
        Vector3 playerPosition = GameManager
            .instance
            .GetPlayerPosition();

        MoveTowardsTarget(playerPosition);
    }

    private void MoveTowardsTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;

        if (go.CompareTag("Projectile"))
        {
            OnEnemyDeath.Invoke(scoreValue);
            Destroy(gameObject);
        }

        if (go.CompareTag("Player"))
        {
            OnEnemyHit.Invoke(damage);
        }
    }
}

