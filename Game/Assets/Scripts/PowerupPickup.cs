using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public GameObject player;
    public GameObject mySelf;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("it hit");
        //Give player buff
       // SaveManager.instance.checkPointSave(player);

        Destroy(mySelf);
    }
}
