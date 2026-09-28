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

    public static event System.Action OnPlayerDied;

    private Vector3 spawnPosition;
    private int startingMaxHealth;

    [Header("Movement")]
    public float speed = 5f;
    public float jumpSpeed = 7f;

    [Header("Jump Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Health")]
    public int startHealth = 100;
    private int _currentHealth;

    public const int AbsoluteMaxHealth = 100;
    public int currentHealth 
    {
        get { return _currentHealth; }
    }

    private bool isDead = false;

    [Header("Shooting")]
    private BulletPool bulletPool;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = -999f;

    [Header("Sounds")]

    public AudioSource sfxSource;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    public bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("After Coroutine");

        _currentHealth = startHealth;

        // build the FSM and start in Idle
        sm = new SimplePlayerStateMachine(this);
        sm.Initialize(sm.idleState);

        bulletPool = FindAnyObjectByType<BulletPool>();
    }

    void Update()
    {
        // the state machine now owns movement + jumping + shoot
        sm.Execute();

     // Debug.Log(_currentHealth);

        
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

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        else
        {
            if (sfxSource != null && hurtSound != null)
                sfxSource.PlayOneShot(hurtSound);
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (deathSound != null) AudioSource.PlayClipAtPoint(deathSound, transform.position);

        OnPlayerDied?.Invoke();

        gameObject.SetActive(false);
    }

public bool CanAttack()
{
    return Time.time - lastAttackTime >= attackCooldown;
}

public void Fire(Vector2 direction)
{
    lastAttackTime = Time.time;
    bulletPool.FireBullet(transform.position, direction);
}
    public void IncreasestartHealth(int amount)
    {
            startHealth += amount;
        if (startHealth > AbsoluteMaxHealth) startHealth = AbsoluteMaxHealth;

        _currentHealth += amount;
        if (_currentHealth > startHealth) _currentHealth = startHealth;
    }

    public void Respawn()
    {
        Debug.Log("Respawn called");
        gameObject.SetActive(true);
        transform.position = spawnPosition;
        rb.linearVelocity = Vector2.zero;

        isDead = false;
        startHealth = startingMaxHealth;
        _currentHealth = startHealth;

        sm.Initialize(sm.idleState);
    }
}