using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float moveSpeed = 5;

    [Range(1, 100)]
    [SerializeField] private float lifeTime = 100;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
