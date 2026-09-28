using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinManagerPool : MonoBehaviour
{

    public GameObject coinPrefab;
    public int amountToPool = 3;

    public AudioSource sfxSource;
    public AudioClip coinSound;

    private ObjectPool<GameObject> coinPool; //ObjectPool is a generic class provided by Unity, gameobject is type
    private List<GameObject> activeCoins = new List<GameObject>();

    private void Awake()
    {
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
       Coin sceneCoin = FindAnyObjectByType<Coin>();
        if (sceneCoin != null)
            sceneCoin.manager = this;
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

        if (sfxSource != null && coinSound != null)
        sfxSource.PlayOneShot(coinSound);
    }

    void OnDisable()
    {
        Debug.Log("CoinManagerPool got disabled!");
    }

    public void ResetCoins()
    {
        foreach (GameObject coin in activeCoins)
        {
            coinPool.Release(coin);
        }
        activeCoins.Clear();

        StartCoroutine(PlaceCoin());
    }
}
