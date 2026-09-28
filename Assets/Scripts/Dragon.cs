using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour, IEnemy
{
    public float speed = 3f;
    public float yOffset = 1f;
    public int damage = 10;
    public float attackCooldown = 2f;

    private float baseSpeed;

    [Header("Animation")]
    public Animator animator;
    public string attackTrigger = "Attack";

    private Player player;

    [Header("Attack Properties")]
    private float lastAttackTime = -999f;

    public float attackRange = 1.5f;

    [Header("Health attributes")]
    public int maxHealth = 35;
    private int currentHealth;
    private bool isDead = false;

    void Start()
    {
        player = FindAnyObjectByType<Player>();

        if (animator == null)
            animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        baseSpeed = speed;
    }

void Update()
{
    if (isDead) return;

    if (player == null) return;

    Vector3 target = player.transform.position + new Vector3(0, yOffset, 0);
    Vector2 direction = (target - transform.position).normalized;
    transform.position += (Vector3)direction * speed * Time.deltaTime;

    if (direction.x > 0.1f)
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    else if (direction.x < -0.1f)
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

    if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        Attack();
}

    public void Attack()
    {
        // Cooldown: prevent spam
        if (Time.time - lastAttackTime < attackCooldown) return;

        if (player != null)
        {
            player.TakeDamage(damage);
            Debug.Log($"Dragon dealt {damage} damage! Player HP: {player.currentHealth}");
        }

        if (animator != null)
        {
            animator.SetTrigger(attackTrigger);
        }

        lastAttackTime = Time.time;
    }

    // Trigger-based: dragon flies INTO player's trigger zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
           // Destroy(gameObject);  // Dragon disappears after one hit
        }
    }

    void OnTriggerStay2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        Attack();
    }
}



    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            Die();

        StartCoroutine(SlowFromHurt());
    }

    IEnumerator SlowFromHurt()
    {
        speed = baseSpeed * 0.3f;
        yield return new WaitForSeconds(0.5f);
        speed = baseSpeed;
    }

    void Die()
    {
        isDead = true;

        if (animator != null)
            animator.SetTrigger("Die");

        ScoreManager.Instance.EnemyDefeated();
        Destroy(gameObject, 1f); // 1 second delay so the death animation gets to play before it disappears
    }
}
