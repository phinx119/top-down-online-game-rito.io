using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject player;
    private PowerUpManager powerUpManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        powerUpManager = GetComponent<PowerUpManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (powerUpManager != null)
            {
                PowerUpManager.PowerUp damagePowerUp = new PowerUpManager.PowerUp(PowerUpManager.PowerUpType.IncreaseDamage, 10);
                powerUpManager.ApplyPowerUp(damagePowerUp);
                Debug.Log("PowerUp applied successfully.");
                Debug.Log("Player's damage: " + player.GetComponent<PlayerStats>().damage);
            }
            else
            {
                Debug.LogError("PowerUpManager is not initialized.");
            }
        }
    }
}
