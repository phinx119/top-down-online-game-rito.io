using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCreate : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Sprite[] buttonImages;
    public ButtonHandler ButtonHandler;
    public GameObject player;
    private List<GameObject> instantiatedButtons = new List<GameObject>();
    private int currLevel;
    private bool buttonsGeneratedForCurrentLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        currLevel = player.GetComponent<PlayerStats>().level;
        if (!buttonsGeneratedForCurrentLevel && currLevel % 3 == 0 && currLevel > 0)
        {
            GeneratePowerUpButton();
            buttonsGeneratedForCurrentLevel = true;
        }
        else if (currLevel % 3 != 0)
        {
            buttonsGeneratedForCurrentLevel = false;
        }
    }

    void GeneratePowerUpButton()
    {
        List<int> randomIndices = GetRandomIndices(buttonImages.Length, 3);
        for (int i = 0; i < randomIndices.Count; i++)
        {
            int index = randomIndices[i];
            GameObject newButton = Instantiate(buttonPrefab, transform);
            instantiatedButtons.Add(newButton);

            ButtonManager manager = newButton.GetComponent<ButtonManager>();
            manager.SetButtonImage(buttonImages[index]);

            Button buttonComponent = newButton.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => ExecuteAction(index));
        }
    }

    List<int> GetRandomIndices(int arrayLength, int count)
    {
        List<int> indices = new List<int>();
        while (indices.Count < count)
        {
            int randomIndex = Random.Range(0, arrayLength);
            if (!indices.Contains(randomIndex))
            {
                indices.Add(randomIndex);
            }
        }
        return indices;
    }

    void DestroyAllButtons()
    {
        foreach (GameObject button in instantiatedButtons)
        {
            Destroy(button);
        }
        instantiatedButtons.Clear();
    }

    void ExecuteAction(int index)
    {
        switch (index)
        {
            case 0:
                ButtonHandler.BuffHealth();
                break;
            case 1:
                ButtonHandler.InstanceHealth();
                break;
            case 2:
                ButtonHandler.BuffShotgunAmmo();
                break;
            case 3:
                ButtonHandler.BuffSpeed();
                break;
            case 4:
                ButtonHandler.BuffDrawHealth();
                break;
            case 5:
                ButtonHandler.BuffIncreaseDrop();
                break;
            case 6:
                ButtonHandler.BuffDamage();
                break;
        }

        DestroyAllButtons();
    }
}
