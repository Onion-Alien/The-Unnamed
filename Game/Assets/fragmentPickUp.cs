using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragmentPickUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.name == "Player" && this.name == "RedFragment1")
        {
            FragmentCount.fc.addRedF1(1);
            Destroy(gameObject);
        }
        else if (other.name == "Player" && this.name == "RedFragment2")
        {
            FragmentCount.fc.addRedF2(1);
            Destroy(gameObject);
        }
        else if (other.name == "Player" && this.name == "GreenFragment1")
        {
            FragmentCount.fc.addGreenF1(1);
            Destroy(gameObject);
        }
        else if (other.name == "Player" && this.name == "GreenFragment2")
        {
            FragmentCount.fc.addGreenF2(1);
            Destroy(gameObject);
        }
    }
}
