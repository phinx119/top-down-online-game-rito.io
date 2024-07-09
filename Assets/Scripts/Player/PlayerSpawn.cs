using Cinemachine;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    private CinemachineVirtualCamera virtualCamera;

    private GameObject playerPrefab;

    void Start()
    {
        playerPrefab = MainMenu.playerPrefab;
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        if (virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
            //virtualCamera.LookAt = player.transform;
        }
    }

    Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
