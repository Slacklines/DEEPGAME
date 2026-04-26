using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animationHandler : MonoBehaviour
{

    public Animator transition;
    private Coroutine loadCoroutine;
    private WaitForSeconds deathDelay = new WaitForSeconds(3f);
    private WaitForSeconds transitionDelay = new WaitForSeconds(1f);
    public void restartLevel()
    {
        Debug.Log("Player death detected");
        if (loadCoroutine != null){StopCoroutine(loadCoroutine);}
        loadCoroutine = StartCoroutine(loadLevel());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("start game");
        if (other.gameObject.tag == "Player")
        {
            deathDelay = new WaitForSeconds(1f);
            if (loadCoroutine != null){StopCoroutine(loadCoroutine);}
            loadCoroutine = StartCoroutine(loadLevel("MainScene"));
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator loadLevel(string sceneName = null)
    {
        yield return deathDelay;

        transition.SetTrigger("Start");

        yield return transitionDelay;

        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
