using UnityEngine;                              

public class CoinManager : MonoBehaviour
{
    private CoinManagerPool pool;

    void Start()
    {
        pool = FindAnyObjectByType<CoinManagerPool>();
    }
}
