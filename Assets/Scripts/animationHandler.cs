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

    IEnumerator loadLevel()
    {
        yield return deathDelay;

        transition.SetTrigger("Start");

        yield return transitionDelay;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
