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
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
