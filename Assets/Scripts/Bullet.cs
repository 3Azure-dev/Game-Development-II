using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletPool manager;   // set by the pool when it fires this bullet
    public float speed = 12f;
    public float lifetime = 2f;

    public int damage = 15;

    private Vector2 direction;
    private float timer;

    // called by Player.Fire() right after getting this bullet from the pool
    public void Launch(Vector2 dir)
    {
        direction = dir;
        timer = 0f;
        transform.right = dir; // points the sprite toward the aim direction
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifetime)
            ReturnToPool();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Dragon dragon = other.GetComponent<Dragon>();
            if (dragon != null)
                dragon.TakeDamage(damage);

            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        if (manager != null) manager.ReleaseBullet(gameObject);
    }
}