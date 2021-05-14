using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropper : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 100f;
    private bool hovering;

    void Start()
    {
        hovering = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        if (Input.GetMouseButton(1))
        {
            transform.position = transform.position + (new Vector3(rb.velocity.x, rb.velocity.y, 0.0f));
            Debug.Log("velocity = "+rb.velocity.ToString());
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void toggleHovering()
    {
        if (hovering)
        {
            hovering = false;
        }
        else
        {
            hovering = true;
        }
    }
}