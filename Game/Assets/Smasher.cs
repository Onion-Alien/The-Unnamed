using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smasher : MonoBehaviour
{
    Vector2 pos, pos2;
    void Start()
    {
        pos = transform.position;
        pos2 = new Vector2(0f, 2f);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, pos2, 1 * Time.deltaTime);
    }

}
