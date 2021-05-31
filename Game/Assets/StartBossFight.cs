using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    private bool hasRun;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasRun)
        {
            hasRun = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.Find("BOSS").gameObject.SetActive(true);
        }
    }
}
