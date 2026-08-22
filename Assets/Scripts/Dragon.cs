using UnityEngine;

public class Dragon : MonoBehaviour, IEnemy
{
    public float speed = 3f;
    public float yOffset = 1f;
    public int damage = 10;
    public float attackCooldown = 2f;

    [Header("Animation")]
    public Animator animator;
    public string attackTrigger = "Attack";

    private Player player;
    private float lastAttackTime = -999f;

    void Start()
    {
        player = FindAnyObjectByType<Player>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

public float attackRange = 1.5f;

void Update()
{
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
}
