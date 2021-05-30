using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Node[] pathNode;
    private int currentNode;
    private GameObject platform;
    public float speed;
    private float timer;
    private Vector3 currentPos, startPos;
    private bool reverse;
    public bool isEnabled;
    public bool singleUse;

    private void Start()
    {
        pathNode = GetComponentsInChildren<Node>();
        platform = transform.Find("Platform").gameObject;
        platform.transform.position = pathNode[0].transform.position;
        CheckNode();
    }

    private void CheckNode()
    {
        timer = 0;
        startPos = platform.transform.position;
        currentPos = pathNode[currentNode].transform.position;
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(transform);
    }
    // Draws spheres on each child node, and draws a line between them (debug)
    private void DrawGizmos(Transform trans)
    {
        for (int i = 0; i < trans.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("Platform")) continue;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.1f);
            Gizmos.color = Color.blue;
            if (i < trans.childCount - 1)
            {
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            }
            DrawGizmos(transform.GetChild(i));
        }
    }

    private void Update()
    {
        isEnabled = GetComponentInChildren<Platform>().colCheck;
        if (isEnabled)
        {
            Move();
        }
    }

    private void Move()
    {
        timer += Time.deltaTime * speed;

        if (platform.transform.position != currentPos)
        {
            platform.transform.position = Vector3.MoveTowards(startPos, currentPos, timer);
        }
        else
        {
            if (!reverse)
            {
                if (currentNode < pathNode.Length - 1)
                {
                    currentNode++;
                    CheckNode();
                }
                else
                {
                    reverse = true;
                }
            }
            if (reverse && !singleUse)
            {
                if (currentNode > 0)
                {
                    currentNode--;
                    CheckNode();
                }
                else
                {
                    reverse = false;
                }
            }
        }
    }
}
