using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameHighlight : MonoBehaviour
{
    public GameObject selectionImage;

    void OnMouseEnter()
    {
        Debug.Log("mouse hovering");
        selectionImage.transform.Translate(gameObject.transform.position);
    }

    void OnMouseOver()
    {
        Debug.Log("mouse hovering");
        selectionImage.transform.Translate(gameObject.transform.position);
    }

    void OnMouseExit()
    {
        Debug.Log("mouse hovering");
        selectionImage.transform.Translate(gameObject.transform.position);
    }
}
