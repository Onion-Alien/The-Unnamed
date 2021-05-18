using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//unused

public class Tunnel : MonoBehaviour
{
    private Color stock;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stock = player.GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.GetComponent<Renderer>().material.color == stock)
        {
            player.GetComponent<Renderer>().material.color = Color.gray;
        }
        else
        {
            player.GetComponent<Renderer>().material.color = stock;
        }
    }
}
    