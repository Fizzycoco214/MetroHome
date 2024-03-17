using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float loadingDelay;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(CoRoutine(loadingDelay, sceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
   

    IEnumerator CoRoutine(float seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }
}
