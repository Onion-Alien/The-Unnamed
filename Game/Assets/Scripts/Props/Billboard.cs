using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script stops sprites from rotating seperatly from player
 */

public class Billboard : MonoBehaviour
{
    Quaternion rotation;
    private void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }
}
