using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTest : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static IEnumerator movespeedBuff(float movementSpeed)
    {
        movementSpeed = movementSpeed * 1.5f;
        yield return new WaitForSeconds(0.5f);
        movementSpeed = movementSpeed / 1.5f;
    }
}
