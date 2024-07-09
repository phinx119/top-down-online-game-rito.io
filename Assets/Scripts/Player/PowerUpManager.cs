using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public enum PowerUpType
    {
        IncreaseDamage,
        IncreaseHealth,
        InstantHealth,
        SpeedBoost,
        ShotgunAmmo,
        IncreaseDrop,
        DrawnHealth
        // Add more PowerUp types as needed
    }

    public class PowerUp
    {
        public PowerUpType PowerUpType { get; private set; }
        public int Value { get; private set; }

        public PowerUp(PowerUpType powerUpType, int value)
        {
            PowerUpType = powerUpType;
            Value = value;
        }
    }

    private List<PowerUp> activePowerUps = new List<PowerUp>();
    private const int maxPowerUps = 7;

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool ApplyPowerUp(PowerUp PowerUp)
    {
        if (activePowerUps.Count >= maxPowerUps)
        {
            Debug.Log("Maximum number of PowerUps reached.");
            return false;
        }

        activePowerUps.Add(PowerUp);
        ApplyPowerUpEffect(PowerUp);
        return true;
    }

    private void RemovePowerUp(PowerUp PowerUp)
    {
        RemovePowerUpEffect(PowerUp);
        activePowerUps.Remove(PowerUp);
    }

    private void ApplyPowerUpEffect(PowerUp PowerUp)
    {
        switch (PowerUp.PowerUpType)
        {
            case PowerUpType.IncreaseDamage:
                // Increase Damage to gun
                player.GetComponent<PlayerStats>().damage += PowerUp.Value;
                break;
            case PowerUpType.InstantHealth:
                // Increase instant Health
                player.GetComponent<PlayerStats>().currHealth += PowerUp.Value;
                break;
            case PowerUpType.IncreaseDrop: 
                // Increase point drop when kill a player
                break;
            case PowerUpType.IncreaseHealth:
                // Increase max Health
                 player.GetComponent<PlayerStats>().maxHealth += PowerUp.Value;
                break;
            case PowerUpType.ShotgunAmmo: 
                // Increase ammo (1 -> 3)
                break;
            case PowerUpType.SpeedBoost:
                // Increase speed
                player.GetComponent<PlayerStats>().speed += PowerUp.Value;
                break;
            case PowerUpType.DrawnHealth: 
                // Get health when deal damage
                break;
        }
    }

    private void RemovePowerUpEffect(PowerUp PowerUp)
    {
        switch (PowerUp.PowerUpType)
        {
            case PowerUpType.IncreaseDamage:
                // Increase Damage to gun
                break;
            case PowerUpType.InstantHealth:
                // Increase instant Health
                break;
            case PowerUpType.IncreaseDrop:
                // Increase point drop when kill a player
                break;
            case PowerUpType.IncreaseHealth:
                // Increase Health
                break;
            case PowerUpType.ShotgunAmmo:
                // Increase ammo (1 -> 3)
                break;
            case PowerUpType.SpeedBoost:
                // Increase speed
                break;
            case PowerUpType.DrawnHealth:
                // Get health when deal damage
                break;
        }
    }

    public List<PowerUp> GetActivePowerUps()
    {
        return activePowerUps;
    }
}
