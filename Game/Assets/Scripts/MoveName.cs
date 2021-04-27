using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveName : MonoBehaviour
{
    public Transform objectToFollow;
    public Text nametag;
    public Vector3 offset;

    private void Start()
    {
        if (SaveManager.instance.getName() != null)
        {
            nametag.text = SaveManager.instance.getName();
        }
    }

    void Update()
    {

        //Move poisition of name above head
        float xPos = objectToFollow.position.x + offset.x;
        float yPos = (objectToFollow.position.y + 1.9f) + offset.y;

        Vector3 newPos = new Vector3(xPos, yPos, 0);

        transform.position = newPos;
    }

    public void setName()
    {

    }
}
