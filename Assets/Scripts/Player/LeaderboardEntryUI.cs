using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] Text playerNameText;
    [SerializeField] Text experienceText;

    public void SetEntry(LeaderboardEntry entry)
    {
        playerNameText.text = entry.playerName;
        experienceText.text = entry.totalExperience.ToString();
    }
}