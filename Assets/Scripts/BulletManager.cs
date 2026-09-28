using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int amountToPool = 5;

    private ObjectPool<GameObject> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool<GameObject>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            false,
            amountToPool,
            10
        );
    }

    // called by Player.Fire() — this is the entire public API of the pool
    public void FireBullet(Vector3 position, Vector2 direction)
    {
        GameObject bullet = bulletPool.Get();
        bullet.transform.position = position;

        Bullet b = bullet.GetComponent<Bullet>();
        b.manager = this;
        b.Launch(direction);
    }

    public void ReleaseBullet(GameObject bullet)
    {
        bulletPool.Release(bullet);
    }

    GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        return bullet;
    }

    void OnGetBullet(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    void OnReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    void OnDestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }
}