using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControlScript : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    public void PlayButtonOnClicked()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitButtonOnClicked() 
    { 
        Application.Quit();
    }
}
