using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField nameInputField;
    public Button playButton;
    public Text warningText;

    public static string playerName;

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        warningText.gameObject.SetActive(false);
    }

    public void PlayGame() 
    {
        if (string.IsNullOrEmpty(nameInputField.text))
        {
            warningText.text = "Name cannot be empty!";
            warningText.gameObject.SetActive(true);
        }
        else
        {
        playerName = nameInputField.text;
        SceneManager.LoadSceneAsync(1);
        }  
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
