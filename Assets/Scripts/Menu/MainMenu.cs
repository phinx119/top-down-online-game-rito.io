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
    public Dropdown serverDropdown;

    public static string playerName;
    public static string selectedServer;
    public static string serverUrl;
    public static string id = System.Guid.NewGuid().ToString();

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
            selectedServer = serverDropdown.options[serverDropdown.value].text;
            HandleServerSelection(selectedServer);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void HandleServerSelection(string server)
    {
        switch (server)
        {
            case "SEA":
                HandleSEAServer();
                break;
            case "NA":
                HandleNAServer();
                break;
            case "EU":
                HandleEUServer();
                break;
            default:
                Debug.Log("Unknown server selected.");
                break;
        }
    }

    void HandleSEAServer()
    {
        Debug.Log("SEA Server selected.");
        serverUrl = "ws://10.33.46.26:8181";
        SceneManager.LoadSceneAsync(2);
    }

    void HandleNAServer()
    {
        Debug.Log("NA Server selected.");
        serverUrl = "wss://websocket-server-kutx.onrender.com";
        SceneManager.LoadSceneAsync(2);
    }

    void HandleEUServer()
    {
        Debug.Log("EU Server selected.");
        SceneManager.LoadSceneAsync(2);
    }
}
