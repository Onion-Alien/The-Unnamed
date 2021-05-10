using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
    }

    public void activateCanvas()
    {
        canvas.SetActive(true);
    }

    public void exitToMain()
    {
        SaveManager.instance.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void exitGame()
    {
        SaveManager.instance.Save();
        Application.Quit();
    }

    public void goBack()
    {
        canvas.SetActive(false);
    }
}
