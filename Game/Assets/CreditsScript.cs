using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    private bool isScrolling = false;
    public GameObject levelLoader;
    public GameObject text;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startCredits());
    }

    // Update is called once per frame
    void Update()
    {
        if (isScrolling)
        {
            text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y + 0.001f, text.transform.position.z);
            counter++;
        }

    }

    IEnumerator startCredits()
    {
        yield return new WaitForSeconds(3.0f);
        toggleScrolling();
        StartCoroutine(creditWait());
    }

    IEnumerator exitToMain()
    {
        yield return new WaitForSeconds(3.0f);
        levelLoader.GetComponent<LevelLoader>().exitToMain();
    }

    private void toggleScrolling()
    {
        isScrolling = true;
    }

    IEnumerator creditWait()
    {
        yield return new WaitForSeconds(16.0f);
        StartCoroutine(exitToMain());
    }
}
