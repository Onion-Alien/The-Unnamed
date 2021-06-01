using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    public GameObject canvas;
    public GameObject button;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnDisable()
    {
        button.SetActive(true);
    }

    public void activateCanvas()
    {
        canvas.SetActive(true);
        button.SetActive(false);
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
        button.SetActive(true);
    }
}
