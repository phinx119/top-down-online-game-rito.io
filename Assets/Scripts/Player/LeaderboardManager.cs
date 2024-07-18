using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Leaderboard")]
    [SerializeField] Transform leaderboardPanel;
    [SerializeField] GameObject leaderboardEntryPrefab;

    List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

    void Start()
    {
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        // Clear existing entries
        foreach (Transform child in leaderboardPanel)
        {
            Destroy(child.gameObject);
        }

        // Sort entries by experience
        leaderboardEntries.Sort((x, y) => y.totalExperience.CompareTo(x.totalExperience));

        // Create new leaderboard entries
        foreach (LeaderboardEntry entry in leaderboardEntries)
        {
            GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardPanel);
            newEntry.GetComponent<LeaderboardEntryUI>().SetEntry(entry);
        }
    }

    public void AddOrUpdateEntry(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        PlayerName playerName = player.GetComponent<PlayerName>();
        LeaderboardEntry entry = leaderboardEntries.Find(e => e.id == player.name);

        if (entry == null)
        {
            entry = new LeaderboardEntry
            {
                player = player,
                playerName = playerName != null ? playerName.nameText.text : "Unknown",
                totalExperience = stats.currExp,
                id = player.name,
            };
            leaderboardEntries.Add(entry);
        }
        else
        {
            entry.totalExperience = stats.currExp;
        }

        UpdateLeaderboard();
    }
}

[System.Serializable]
public class LeaderboardEntry
{
    public GameObject player;
    public string playerName;
    public int totalExperience;
    public string id = "Player";
}