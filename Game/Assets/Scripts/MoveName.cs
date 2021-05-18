using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveName : MonoBehaviour
{
    public Transform objectToFollow;
    public Text nametag;
    public Vector3 offset;

    public float nameToggle;

    private void Start()
    {
        nameToggle = 0;
        getName();
    }

    void Update()
    {
        //Toggle name on/off
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    toggleName();
        //}
        //Move poisition of name above head
        float xPos = objectToFollow.position.x + offset.x;
        float yPos = objectToFollow.position.y + (offset.y  + 2.30f) + nameToggle;
        float zPos = objectToFollow.position.z + offset.z;

        Vector3 newPos = new Vector3(xPos, yPos, zPos);

        nametag.transform.position = newPos;
    }

    public void toggleName()
    {
        if (nameToggle == 0)
        {
            nameToggle = 20;
        }
        else
        {
            nameToggle = 0;
        }
    }

    //Get name string from SaveManager object
    public void getName()
    {
        if (SaveManager.instance.getName() != null)
        {
            nametag.text = SaveManager.instance.getName();
        }
    }
}
