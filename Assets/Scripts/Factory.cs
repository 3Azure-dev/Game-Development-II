using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject dragonPrefab;
    public GameObject zombiePrefab;
    public GameObject monsterPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

   public IEnemy GetEnemy(EnemyType.EnemyEnum enemyType){
    IEnemy enemy = null;
    GameObject spawned = null;

        switch (enemyType)
        {
            case EnemyType.EnemyEnum.Dragon:
                spawned = Instantiate(dragonPrefab);
                enemy = spawned.GetComponent<IEnemy>();
                ApplyDragonDifficulty(spawned);
                Debug.Log("Factory spawned a Dragon!");
                break;

            case EnemyType.EnemyEnum.Zombie:
                spawned = Instantiate(zombiePrefab);
                enemy = spawned.GetComponent<IEnemy>();
                Debug.Log("Factory spawned a Zombie!");
                break;

            case EnemyType.EnemyEnum.Monster:
                spawned = Instantiate(monsterPrefab);
                enemy = spawned.GetComponent<IEnemy>();
                Debug.Log("Factory spawned a Monster!");
                break;

            default:
                Debug.LogError("Unknown enemy type: " + enemyType);
                break;
        }
    
    if (spawned != null) ScoreManager.Instance.EnemySpawned();

    return enemy;
    }

    void ApplyDragonDifficulty(GameObject spawned)
{
    Dragon dragon = spawned.GetComponent<Dragon>();
    if (dragon == null) return;

    int tier = ScoreManager.Instance.Count / 5; // 0-4 coins = tier 0, 5-9 = tier 1, etc, uncapped
    dragon.maxHealth += tier * 10;
    dragon.speed += tier * 0.5f;
}
}
