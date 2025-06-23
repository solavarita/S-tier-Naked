using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Sprite pauseSprite;
    public Sprite resumeSprite;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public GameObject grayScreen;
    public Text pausedText;
    public AudioSource musicBGM;
    public AudioSource foodSFX;
 
    public Button musicButton;
    public Button pauseButton;  
    public Button resumeButton;
    public Button quitButton;

    private bool isMusicOn = true;
    private bool isPaused = false;


    private void Start()
    {
        
    }
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        Debug.Log(isMusicOn);

        if (isMusicOn == true)
        {
            musicButton.image.sprite = musicOnSprite;
            musicBGM.UnPause();
            
        }
        else
        {
            musicButton.image.sprite = musicOffSprite;
            musicBGM.Pause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        Debug.Log(isPaused);
        if (isPaused == true)
        {
            pauseButton.image.sprite = resumeSprite;
            Time.timeScale = 0;
            grayScreen.SetActive(true);
            pausedText.gameObject.SetActive(true);
            resumeButton.gameObject.SetActive(true);
        }
        else
        {
            pauseButton.image.sprite = pauseSprite;
            Time.timeScale = 1;
            grayScreen.SetActive(false);
            pausedText.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(false);
        }
    }

    public void QuitButtonOnClicked()
    {

    }

}
