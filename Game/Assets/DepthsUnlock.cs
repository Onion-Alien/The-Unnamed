using UnityEngine;

public class DepthsUnlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SaveManager.instance.UnlockNextLevel();
    }
}