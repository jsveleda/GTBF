using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private float horizontalMovement;
    private float verticalMovement;

    private Vector2 mousePosition;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
    private float nextFire = 0f;

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

        if (Input.GetMouseButtonDown(0) && nextFire <= 0f)
        {
            Shoot();
            nextFire = fireRate;
        }
        else
        {
            nextFire -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = new (horizontalMovement, verticalMovement);
        rb.velocity = targetPosition.normalized * moveSpeed;
    }
}
