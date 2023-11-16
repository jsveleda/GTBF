using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int damage = 1;

    private void Update()
    {
        Vector3 playerPosition = GameManager
            .instance
            .GetPlayerPosition();

        float distanceToPlayer = Vector3
            .Distance(transform.position, playerPosition);

        MoveTowardsTarget(playerPosition, distanceToPlayer);
    }

    private void MoveTowardsTarget(Vector3 target, float minDistance)
    {
        if (minDistance > 1.5f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var go = collision.gameObject;

        if (go.CompareTag("Projectile"))
        {
            //If this enemy hits a projectile he dies
            GameManager.instance.IncrementScore(2);
            Destroy(gameObject);
        }

        if (go.CompareTag("Player"))
        {
            //If this enemy hits the player, it causes damage
            GameManager.instance.EnemyHitPlayer(damage);
        }
    }
}

