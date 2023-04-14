using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator MenuScreenFade;
    public string gameScene;

    public void startGame(){
        MenuScreenFade.SetBool("StartedGame",true);
        StartCoroutine(switchScenes(3));
    }
    public void quitGame(){
        Application.Quit();
        Debug.Log("Quitting");
    }
    IEnumerator switchScenes(int secs)
    {
        yield return new WaitForSeconds(secs);
        SceneManager.LoadScene(gameScene);
    }
}
