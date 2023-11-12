using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject Player;

    [SerializeField] 
    private float moveSpeed = 5;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            Player.transform.position,
            moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            GameManager.instance.IncrementScore(2);
            Destroy(gameObject);
        }
    }
}

