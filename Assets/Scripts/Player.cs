using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Jump Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Health")]
    public int maxHealth = 100;
    private int _currentHealth;
    public int currentHealth 
    {
        get { return _currentHealth; }
    }

    private bool isGrounded;
    private int count;
    public int Count { get 
    { 
        return count; 
    }
    set
        {
            count = value;
        }
    }
    private CoinManagerPool coinPool;
    public float coinSpawnDistance = 2f;
    public GameObject coinPrefab;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("After Coroutine");
        coinPool  = FindAnyObjectByType<CoinManagerPool>();

        _currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocityY = jumpForce;
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            SpawnCoinAhead();
        }

        Debug.Log(_currentHealth);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);
        rb.linearVelocity = movement;

        if (animator != null)
        {
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
            animator.SetFloat("yVelocity", rb.linearVelocity.y);
        }

        if (spriteRenderer != null)
        {
        if (moveHorizontal > 0) spriteRenderer.flipX = false;
        else if (moveHorizontal < 0) spriteRenderer.flipX = true;
        }
    }

    IEnumerator Counter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Count++;
            Debug.Log("Count: " + Count);
        }
    }

    public void StartCounterCoroutine()
    {
        StartCoroutine(Counter());
    }

    void SpawnCoinAhead()
    {
        if (coinPool != null)
        {
            float direction = spriteRenderer.flipX ? -1f : 1f;
            Vector3 spawnPos = transform.position + new Vector3(direction * coinSpawnDistance, 0, 0);
            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}