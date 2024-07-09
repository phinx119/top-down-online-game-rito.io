using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField nameInputField;
    public Button playButton;
    public Text warningText;
    public GameObject player;
    public static GameObject playerPrefab;

    public static string playerName;

    void Start()
    {
        playerPrefab = player;
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
            SceneManager.LoadSceneAsync(2);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
