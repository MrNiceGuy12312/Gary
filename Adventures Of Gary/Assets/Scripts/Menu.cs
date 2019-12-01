using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 

    public void MainMenu ()
    {
        SceneManager.LoadScene(0);
    }
    public void Play ()
	{
        SceneManager.LoadScene(1); 
	}
    public void HowToPlay ()
    {
        SceneManager.LoadScene(2);
    }
    public void Settings ()
    {
        SceneManager.LoadScene(3);
    }
    public void Credits ()
    {
        SceneManager.LoadScene(4);
    }
	 public void GameOver ()
    {
        SceneManager.LoadScene(5);
    }



    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
	
    
}
