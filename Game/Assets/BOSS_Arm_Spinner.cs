using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_Arm_Spinner : MonoBehaviour
{

    public Transform[] movePoints;
    public float speed;
    private int amount;
    private Transform currentTarget;
    private float timer = 0f;

    void Start()
    {
    }

    void Update()
    {
        Movement();

        randoming();
    }

    void randoming()
    {
        amount = Random.Range(0, movePoints.Length);
        currentTarget = movePoints[amount];
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
    }
}
