using UnityEngine;

public class InstantKillSpikes : MonoBehaviour
{
    public bool shoot;
    private void Awake()
    {
        transform.localScale = new Vector3(Random.Range(0.7f, 1.2f), 1, 1);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + Random.Range(-10f, 10f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(100, true);
        }
    }
}
