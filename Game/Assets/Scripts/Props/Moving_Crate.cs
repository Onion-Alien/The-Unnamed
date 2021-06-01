using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Crate : MonoBehaviour
{
    private Vector2 pos1;
    private Vector2 pos2;
    public int x;
    public int y;
    public float speed;
    public bool spinningPlatform;
    public bool platform;

    private void Awake()
    {
        if (spinningPlatform)
        {
            if (!GetComponent<Rigidbody2D>())
            {
                gameObject.AddComponent<Rigidbody2D>();
            }
            gameObject.GetComponent<Rigidbody2D>().mass = 10;
            gameObject.GetComponent<Rigidbody2D>().angularDrag = 1;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            Vector2 temp = transform.position;
            pos1 = temp - new Vector2(x, y);
            pos2 = pos1 + new Vector2(x, y) * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!spinningPlatform)
        {
            transform.position = Vector2.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && platform)
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && platform)
        {
            collision.collider.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(transform);
    }

    private void DrawGizmos(Transform trans)
    {
        Vector2 temp = transform.position;
        Vector2 temp1 = temp - new Vector2(x, y);
        Vector2 temp2 = temp1 + new Vector2(x, y) * 2;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(temp1, temp2);
        if (y == 0)
        {
            Gizmos.DrawCube(temp1, new Vector3(0.2f, 2));
            Gizmos.DrawCube(temp2, new Vector3(0.2f, 2));
        }
        else
        {
            Gizmos.DrawCube(temp1, new Vector3(2, 0.2f));
            Gizmos.DrawCube(temp2, new Vector3(2, 0.2f));
        }
    }
}
