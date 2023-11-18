using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float healthPoints = 10f;
    [SerializeField] private float moveSpeed;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;

    [SerializeField] private Transform fireOriginPoint;
    [SerializeField] private GameObject projectilePrefab;

    private Rigidbody2D rb;
    private float horizontalMovement;
    private float verticalMovement;

    private Vector2 mousePosition;
    private float fireRateCountDown = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float deltaY = mousePosition.y - transform.position.y;
        float deltaX = mousePosition.x - transform.position.x;
        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (ShotBindPressed() && fireRateCountDown <= 0f)
        {
            Shoot();
            fireRateCountDown = fireRate;
        }
        else
        {
            fireRateCountDown -= Time.deltaTime;
        }
    }

    private static bool ShotBindPressed()
    {
        return Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, fireOriginPoint.position, fireOriginPoint.rotation);
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = new (horizontalMovement, verticalMovement);
        rb.velocity = targetPosition.normalized * moveSpeed;
    }

    public void TakeDamage(int damagePoints)
    {
        healthPoints -= damagePoints;
    }

    public float GetHealthPoints()
    {
        return healthPoints;
    }
}
