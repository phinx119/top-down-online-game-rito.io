using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject player;
    private PowerUpManager powerUpManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        powerUpManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpManager>();
    }

    public void BuffDamage()
    {
        if (powerUpManager != null)
        {
            PowerUpManager.PowerUp damagePowerUp = new PowerUpManager.PowerUp(PowerUpManager.PowerUpType.IncreaseDamage, 3);
            powerUpManager.ApplyPowerUp(damagePowerUp);
            Debug.Log("PowerUp applied successfully.");
            Debug.Log("Player's damage: " + player.GetComponent<PlayerStats>().damage);
        }
        else
        {
            Debug.LogError("PowerUpManager is not initialized.");
        }
    }

    public void BuffHealth()
    {
        if (powerUpManager != null)
        {
            PowerUpManager.PowerUp healthPowerUp = new PowerUpManager.PowerUp(PowerUpManager.PowerUpType.IncreaseHealth, 10);
            powerUpManager.ApplyPowerUp(healthPowerUp);
            Debug.Log("PowerUp applied successfully.");
            Debug.Log("Player's max Health: " + player.GetComponent<PlayerStats>().maxHealth);
        }
        else
        {
            Debug.LogError("PowerUpManager is not initialized.");
        }
    }

    public void InstanceHealth()
    {
        if (powerUpManager != null)
        {
            PowerUpManager.PowerUp instantHealthPowerUp = new PowerUpManager.PowerUp(PowerUpManager.PowerUpType.InstantHealth, 10);
            powerUpManager.ApplyPowerUp(instantHealthPowerUp);
            Debug.Log("PowerUp applied successfully.");
            Debug.Log("Player's current Health: " + player.GetComponent<PlayerStats>().currHealth);
        }
        else
        {
            Debug.LogError("PowerUpManager is not initialized.");
        }
    }

    public void BuffSpeed()
    {
        if (powerUpManager != null)
        {
            PowerUpManager.PowerUp speedPowerUp = new PowerUpManager.PowerUp(PowerUpManager.PowerUpType.SpeedBoost, 5);
            powerUpManager.ApplyPowerUp(speedPowerUp);
            Debug.Log("PowerUp applied successfully.");
            Debug.Log("Player's speed: " + player.GetComponent<PlayerStats>().speed);
        }
        else
        {
            Debug.LogError("PowerUpManager is not initialized.");
        }
    }

    public void BuffShotgunAmmo()
    {
        if (powerUpManager != null)
        {
            PowerUpManager.PowerUp shotgunPowerUp = new PowerUpManager.PowerUp(PowerUpManager.PowerUpType.ShotgunAmmo, 10);
            powerUpManager.ApplyPowerUp(shotgunPowerUp);
            Debug.Log("PowerUp applied successfully.");
            Debug.Log("Player's current Health: " + player.GetComponent<PlayerStats>().currHealth);
        }
        else
        {
            Debug.LogError("PowerUpManager is not initialized.");
        }
    }

    public void BuffLongerRange()
    {
        if (powerUpManager != null)
        {
            PowerUpManager.PowerUp longerRangePowerUp = new PowerUpManager.PowerUp(PowerUpManager.PowerUpType.LongerRange, 1);
            powerUpManager.ApplyPowerUp(longerRangePowerUp);
            Debug.Log("PowerUp applied successfully.");
            Debug.Log("Bullet Range: " + player.GetComponent<PlayerStats>().range);
        }
        else
        {
            Debug.LogError("PowerUpManager is not initialized.");
        }
    }

    public void BuffIncreaseDrop()
    {
        if (powerUpManager != null)
        {
            PowerUpManager.PowerUp increaseDropPowerUp = new PowerUpManager.PowerUp(PowerUpManager.PowerUpType.IncreaseDrop, 10);
            powerUpManager.ApplyPowerUp(increaseDropPowerUp);
            Debug.Log("PowerUp applied successfully.");
            Debug.Log("Player's current Health: " + player.GetComponent<PlayerStats>().currHealth);
        }
        else
        {
            Debug.LogError("PowerUpManager is not initialized.");
        }
    }

    public void BuffDrawHealth()
    {
        if (powerUpManager != null)
        {
            PowerUpManager.PowerUp drawnHealthPowerUp = new PowerUpManager.PowerUp(PowerUpManager.PowerUpType.DrawnHealth, 2);
            powerUpManager.ApplyPowerUp(drawnHealthPowerUp);
            Debug.Log("PowerUp applied successfully.");
            Debug.Log("Player's current Health: " + player.GetComponent<PlayerStats>().currHealth);
        }
        else
        {
            Debug.LogError("PowerUpManager is not initialized.");
        }
    }
}
