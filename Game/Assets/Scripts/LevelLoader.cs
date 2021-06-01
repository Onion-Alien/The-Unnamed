using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public float transitionTime = 1f;

    public void Start()
    {
        //if (SceneManager.GetActiveScene().buildIndex > 0)
        //{
        //    playerSpawn();
        //}
    }

    public void playerRespawn()
    {
        SceneManager.LoadScene(SaveManager.instance.getCurrentLevel());
        playerSpawn();
    }

    public void loadNextLevel()
    {
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void exitToMain()
    {
        //this wont work from credits if mainmenu wasnt the starting point
        SaveManager.instance.Save();
        StartCoroutine(goToMain());
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
            player.transform.position = new Vector3(-77.18f, 90.53f, 0.0f);
        }
    }

    IEnumerator LoadNextLevel(int index)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);


        SceneManager.LoadScene(index);
    }

    public IEnumerator LoadNextLevelString(string level)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }

    IEnumerator goToMain()
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
      
        SceneManager.LoadScene("MainMenu");
    }

    public void loadFromMain()
    {
        if (SaveManager.instance.checkSaveExist())
        {
            SaveManager.instance.Load();
            StartCoroutine(LoadNextLevelString(SaveManager.instance.getCurrentLevel()));

        }
    }
}