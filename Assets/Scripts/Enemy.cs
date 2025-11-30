using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AudioClip whatTheDirin;
    private AudioSource audioSource;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;          // Speed of enemy movement
    public float moveRange = 3f;          // Range of random movement
    private float leftBoundary;
    private float rightBoundary;
    private Vector2 moveDirection;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;   // Projectile to shoot
    public float shootInterval = 2f;      // Time between shots
    public float projectileSpeed = 5f;    // Speed of the projectile
    public Transform firePoint;           // Point where projectiles spawn

    [Header("Detection Settings")]
    public float detectionRange = 5f;     // Range within which the enemy detects the player

    private Transform player;
    private Rigidbody2D rb;

    public GameObject openMouth;          // Open mouth GameObject

    private Vector3 originalScale;        // Store the original scale of the enemy

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Freeze the Z-axis rotation to keep the enemy upright
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Set up boundaries for random movement
        float startX = transform.position.x;
        leftBoundary = startX - moveRange;
        rightBoundary = startX + moveRange;

        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Store the original scale of the enemy
        originalScale = transform.localScale;

        // Start movement and shooting behaviors
        StartCoroutine(RandomMovement());

    // Start shooting coroutine with random initial delay
        float initialDelay = Random.Range(0f, shootInterval);
        StartCoroutine(ShootAtPlayerWithDelay(initialDelay));

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MoveEnemy();
    }

    // Handles random back-and-forth movement
    IEnumerator ShootAtPlayerWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(ShootAtPlayer());
    }
    IEnumerator RandomMovement()
    {
        while (true)
        {
            // Randomize movement direction (-1 or 1)
            moveDirection = new Vector2(Random.Range(-1f, 1f), 0).normalized;

            // Wait for a random time before changing direction
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    void MoveEnemy()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // Flip direction if out of bounds
        if (transform.position.x <= leftBoundary)
        {
            moveDirection = Vector2.right;
        }
        else if (transform.position.x >= rightBoundary)
        {
            moveDirection = Vector2.left;
        }

        // Flip sprite based on movement direction (only modify the X scale)
        if (moveDirection.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection.x) * originalScale.x, originalScale.y, originalScale.z);
        }
    }

    // Handles shooting projectiles toward the player
    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
            {
                openMouth.SetActive(true);
                ShootProjectile();
            }
            yield return new WaitForSeconds(shootInterval / 2);
            openMouth.SetActive(false);
            yield return new WaitForSeconds(shootInterval / 2);
        }
    }

    void ShootProjectile()
    {
        // Instantiate the projectile
        audioSource.clip = whatTheDirin;
        audioSource.Play();
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Calculate direction to the player
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Apply force to the projectile using the projectileSpeed variable
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        // Optionally, destroy the projectile after some time
        Destroy(projectile, 5f);
    }
}
