using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{ 

	

    public void MainMenu ()
    {
        SceneManager.LoadScene( 0);

    }

    public void levelOne ()
    {
        SceneManager.LoadScene( 2);
    }

    public void levelTwo ()
    {
        SceneManager.LoadScene( 3);
    }

    public void levelThree ()
    {
        SceneManager.LoadScene( 4);
    }

    public void levelFour ()
    {
        SceneManager.LoadScene( 5);
    }

    public void levelFive ()
    {
        SceneManager.LoadScene( 6);
    }

    public void Intro ()
    {
        SceneManager.LoadScene( 7);
    }
    
    public void Credits ()
    {
        SceneManager.LoadScene( 8);
    }


    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
