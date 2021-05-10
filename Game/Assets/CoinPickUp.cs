using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{

    public enum enemyType { GOBLIN, SKELETON, MUSHROOM, FLYINGEYE };
    public enemyType current;

    

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.name == "Player")
        {
           
                if (current == enemyType.GOBLIN)
                {
                 GoldCount.gc.addGold(70);
                 Destroy(gameObject);
            }
                else if (current == enemyType.MUSHROOM)
                {
                GoldCount.gc.addGold(80);
                Destroy(gameObject);
            }
                else if (current == enemyType.SKELETON)
                {
                GoldCount.gc.addGold(100);
                Destroy(gameObject);
            }
                else if (current == enemyType.FLYINGEYE)
                {
                GoldCount.gc.addGold(50);
                Destroy(gameObject);
            }

               
       
        }
    }
}
