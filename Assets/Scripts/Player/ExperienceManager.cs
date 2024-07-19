using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;
    public GameObject player;

    int currentLevel, totalExperience;
    int previousLevelsExperience, nextLevelsExperience;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;

    LeaderboardManager leaderboardManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        leaderboardManager = FindObjectOfType<LeaderboardManager>();
        UpdateLevel();
    }

    void Update()
    {
        AddExperience();
    }

    public void AddExperience()
    {
        if (player != null)
        {
            totalExperience = player.GetComponent<PlayerStats>().currExp;
            CheckForLevelUp();
            UpdateInterface();
            leaderboardManager.AddOrUpdateEntry(player);
        }
    }

    void CheckForLevelUp()
    {
        if (totalExperience >= nextLevelsExperience)
        {
            currentLevel++;
            player.GetComponent<PlayerStats>().level = currentLevel;
            UpdateLevel();

            // Start level up sequence... Possibly vfx?
        }
    }

    void UpdateLevel()
    {
        previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
        UpdateInterface();
    }

    void UpdateInterface()
    {
        int start = totalExperience - previousLevelsExperience;
        int end = nextLevelsExperience - previousLevelsExperience;

        levelText.text = currentLevel.ToString();
        experienceText.text = start + " exp / " + end + " exp";
        experienceFill.fillAmount = (float)start / (float)end;
    }
}
