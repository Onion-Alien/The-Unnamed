using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject player;

    public void Start()
    {
        playerSpawn();
    }

    public void playerRespawn()
    {
        SceneManager.LoadScene(SaveManager.instance.getCurrentLevel());
        playerSpawn();
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void exitToMain()
    {
        SaveManager.instance.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        SaveManager.instance.Save();
        Application.Quit();
    }

    public void playerSpawn()
    {
        if (SaveManager.instance.x != 0.0f && SaveManager.instance.y != 0.0f)
        {
            player.transform.position = new Vector3(SaveManager.instance.x, SaveManager.instance.y, 0.0f);
        }
        else
        {
            player.transform.position = new Vector3(-52.8f, 82.0f, 0.0f);
        }
    }
}