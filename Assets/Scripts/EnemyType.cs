using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public Factory factory;
    public enum EnemyEnum
    {
        Zombie,
        Dragon,
        Monster
    }

    public EnemyEnum enemy;

    void Start()
    {
        if (factory == null)
            factory = GetComponent<Factory>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger entered by: {collision.gameObject.name}, tag: {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("Player"))
        {
            if (factory == null)
            {
                Debug.LogError("FACTORY IS NULL! Drag Factory GameObject into the 'Factory' slot.");
                return;
            }

            Debug.Log($"Spawning enemy type: {enemy}");
            IEnemy spawnedEnemy = factory.GetEnemy(this.enemy);
            Debug.Log($"Spawned enemy: {spawnedEnemy}");
        }
    }
}
