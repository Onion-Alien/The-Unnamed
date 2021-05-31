using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerCombat>().canInteract = true;
            GetComponentInChildren<PortalInteractGlow>().isEnabled = true;
        }
    }

    public void Interact()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerCombat>().canInteract = false;
            GetComponentInChildren<PortalInteractGlow>().isEnabled = false;
        }
    }
}
