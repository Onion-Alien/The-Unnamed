using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelDoor : MonoBehaviour
{
    private BOSS_Head boss;
    void Start()
    {
        boss = GameObject.Find("Head").GetComponent<BOSS_Head>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null)
        {
            if (boss.health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
