using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToHubMenu : MonoBehaviour
{
    public GameObject canvas;
    public GameObject resumeButton;
    public GameObject optionButton;
    public GameObject exitButton;
    public GameObject hubExitButton;
    public GameObject selectionImage;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void Update()
    {
        if(canvas.activeSelf == true)
        {
            resumeButton.SetActive(false);
            optionButton.SetActive(false);
            exitButton.SetActive(false);
            selectionImage.SetActive(false);
        }
            
    }
        
    private void OnDisable()
    {
        resumeButton.SetActive(true);
        optionButton.SetActive(true);
        exitButton.SetActive(true);
        hubExitButton.SetActive(true);
        selectionImage.SetActive(true);
    }

    public void activateCanvas()
    {
        resumeButton.SetActive(false);
        optionButton.SetActive(false);
        exitButton.SetActive(false);
        exitButton.SetActive(false);
        canvas.SetActive(true);
    }

    public void exitToHub()
    {
        SaveManager.instance.Save();
        SceneManager.LoadScene("World-Hub");
    }

    public void goBack()
    {
        resumeButton.SetActive(true);
        optionButton.SetActive(true);
        exitButton.SetActive(true);
        hubExitButton.SetActive(true);
        selectionImage.SetActive(true);
        canvas.SetActive(false);
    }
}
