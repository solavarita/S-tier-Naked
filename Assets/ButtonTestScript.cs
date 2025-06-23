using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTestScript : MonoBehaviour
{
    public Button testButton;
    public Text buttonText;
    int counter = 0;
    public void ButtonOnClicked()
    {
        Debug.Log("Button Clicked");
        counter++;
        buttonText.text = counter.ToString();

    }
}
