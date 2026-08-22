using UnityEngine;
                              

public class Coin : MonoBehaviour
{
    public CoinManagerPool manager;    // Set by CoinManagerPool during spawning

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && manager != null)
        {
            manager.CollectCoin(gameObject);
        }
    }
}

