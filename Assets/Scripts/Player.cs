using UnityEngine;
using System.Collections;
using DesignPatterns.StatePattern;


public class Player : MonoBehaviour
{
    // public so the state objects (which live outside this class) can read them
    public Rigidbody2D rb;
    public Animator anim;
    private SpriteRenderer spriteRenderer;

    public SimplePlayerStateMachine sm;

    [Header("Movement")]
    public float speed = 5f;
    public float jumpSpeed = 7f;

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

    public bool isGrounded;
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
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("After Coroutine");
        coinPool  = FindAnyObjectByType<CoinManagerPool>();

        _currentHealth = maxHealth;

        // build the FSM and start in Idle
        sm = new SimplePlayerStateMachine(this);
        sm.Initialize(sm.idleState);
    }

    void Update()
    {
        // the state machine now owns movement + jumping
        sm.Execute();

        if (Input.GetKeyDown(KeyCode.F)) {
            SpawnCoinAhead();
        }

        Debug.Log(_currentHealth);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // the states drive the Rigidbody now; this only reads input
        // for the animator params and sprite facing
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (anim != null)
        {
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));
            anim.SetFloat("yVelocity", rb.linearVelocity.y);
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