using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    private Color stock;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        stock = collision.GetComponent<Renderer>().material.color;
        collision.GetComponent<Renderer>().material.color = Color.gray;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Renderer>().material.color = stock;
    }
}
    