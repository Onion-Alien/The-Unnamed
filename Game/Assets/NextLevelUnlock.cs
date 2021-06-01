using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelUnlock : MonoBehaviour
{
    private EnemyHit ninja;
    void Start()
    {
        ninja = GameObject.Find("Ninja").GetComponent<EnemyHit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ninja.currentHealth <= 0)
        {
            gameObject.SetActive(false);
            SaveManager.instance.UnlockNextLevel();
        }
    }
}
