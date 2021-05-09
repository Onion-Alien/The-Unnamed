using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int coin;
    
    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.name == "Player")
        {
            GoldCount.gc.addGold(50);
            Destroy(gameObject);
        }
    }
}
