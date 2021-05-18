using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHover : MonoBehaviour
{
    public float hoverAmount, counter;
    private bool direction;
    public GameObject powerup;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (new Vector3(0.0f, hoverAmount, 0.0f));
        counter++;
        if(counter > 100)
        {
            hoverAmount =  -1.0f * hoverAmount;
            counter = 0;
        }
        
    }
}
