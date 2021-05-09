using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smasher : MonoBehaviour
{
    private Vector2 retract, extend;
    private bool isExtended = true;
    private float temp;
    public float repeatDelay;
    void Start()
    {
        retract = transform.position;
        extend = new Vector2(retract.x, retract.y - 2.5f);
        if (Mathf.Approximately(GetComponentInParent<Transform>().rotation.eulerAngles.z, 180f))
        {
            extend = new Vector2(retract.x, retract.y - 2.5f);
        }
        else
        {
            extend = new Vector2(retract.x, retract.y + 2.5f);
        }
    }

    private void Update()
    {
        temp += Time.deltaTime;
        if (!isExtended && temp >= repeatDelay)
        {
            StartCoroutine("Extend");
        }
        if (isExtended)
        {
            temp = 0;
            StartCoroutine("Retract");
        }

    }

    private void Extend()
    {
        if (transform.position.y != extend.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, extend, 25 * Time.deltaTime);
        }
        else
        {
            isExtended = true;
        }
    }

    private void Retract()
    {
        if (transform.position.y != retract.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, retract, 10 * Time.deltaTime);
        }
        else
        {
            isExtended = false;
        }
    }

}
