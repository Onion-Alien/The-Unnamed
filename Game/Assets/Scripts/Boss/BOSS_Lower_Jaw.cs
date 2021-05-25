using UnityEngine;

public class BOSS_Lower_Jaw : MonoBehaviour
{
    public Transform idlePoint;
    private Vector3 randomPos;


    private bool isActive = true;

    private void Start()
    {
        randomPos = idlePoint.position + (Vector3)Random.insideUnitCircle * 0.3f;
    }

    private void Update()
    {
        //transform.Rotate(0, 0, 500 * Time.deltaTime);
        if (isActive)
        {
            Move();
        }
    }

    private void Move()
    {

        if (transform.position != randomPos)
        {
            //transform.position = Vector3.MoveTowards(idlePoint.position, randomPos, 1);
            transform.position = Vector2.MoveTowards(transform.position, randomPos, 10f * Time.deltaTime);
        }
        if (transform.position == randomPos)
        {
            randomPos = idlePoint.position + (Vector3)Random.insideUnitCircle * 0.3f;
        }
    }
}
