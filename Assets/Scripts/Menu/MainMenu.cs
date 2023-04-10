using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameScene;

    public void startGame(){
        SceneManager.LoadScene(gameScene);
    }
    public void quitGame(){
        Application.Quit();
        Debug.Log("Quitting");
    }
}
