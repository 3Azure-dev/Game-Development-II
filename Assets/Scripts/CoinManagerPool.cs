using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinManagerPool : MonoBehaviour
{

    public GameObject coinPrefab;
    public int amountToPool = 3;

    private ObjectPool<GameObject> coinPool; //ObjectPool is a generic class provided by Unity, gameobject is type
    private List<GameObject> activeCoins = new List<GameObject>();

    Action<int> example; //delegate which points to a method

Action exampleTwo;

Func<int> exampleFunction;

int exampleThree()
        {
            return 1;
        }
void ExampleTwo()
        {
            
        }

        void ExampleFour()
        {
            
        }
    void ActionExample(int a)
    {
        Debug.Log("Action called");
    }

    private void Awake()
    {
        example = ActionExample; //reference to function
        exampleTwo += ExampleTwo;
        exampleTwo += ExampleFour;
        exampleFunction = exampleThree;

        example.Invoke(1);
        exampleTwo.Invoke();
;

        //managing pool
        coinPool = new ObjectPool<GameObject>(
            CreateCoin, 
            OnGetCoin, 
            OnReleaseCoin,
            OnDestroyCoin,
            false,
            amountToPool,
            10
        );
    }

    private void Start()
    {
       StartCoroutine(PlaceCoin());
    }

    private IEnumerator PlaceCoin()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject coin = coinPool.Get();
            coin.transform.position = transform.position + new Vector3(i, -1, 0);
            coin.GetComponent<Coin>().manager = this;
            activeCoins.Add(coin);
        }
    }

    GameObject CreateCoin()
    {
        Debug.Log("Instantiate coins");
        GameObject coin = Instantiate(coinPrefab);
        return coin;
    }

    void OnGetCoin(GameObject coin)
    {
        Debug.Log("Get coin");
        coin.SetActive(true);
    }

    void OnReleaseCoin(GameObject coin)
    {
        Debug.Log("Release coins");
        coin.SetActive(false);
    }

    void OnDestroyCoin(GameObject coin)
    {
        Debug.Log("Destroy coins");
        Destroy(coin);
    }

    public void CollectCoin(GameObject coin)
    {
        activeCoins.Remove(coin);
        coinPool.Release(coin);
        var count = ScoreManager.Instance.CoinCount();
        if(count % 3 == 0)
        StartCoroutine(PlaceCoin());
        Debug.Log("Coins Left: " + activeCoins.Count);
    }
}
