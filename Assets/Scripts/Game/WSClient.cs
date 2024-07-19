using System;
using System.Collections;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WSClient : MonoBehaviour
{
    private ClientWebSocket websocket;
    public string url = "wss://websocket-server-kutx.onrender.com";
    public float sendInterval = 0.1f;
    private GameObject player;
    private string playerId;
    private string playerName;
    public GameObject playerEnemyPrefab;
    public GameObject leaderboard;
    private LeaderboardManager leaderboardManager;
    private PlayerStats playerStats;
    private bool isDisconnected = false;
    async void Start()
    {
        string inputUrl = MainMenu.serverUrl;
        player = GameObject.FindGameObjectWithTag("Player");
        playerId = MainMenu.id;
        playerName = MainMenu.playerName;

        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
        leaderboardManager = leaderboard.GetComponent<LeaderboardManager>();

        if (!String.IsNullOrEmpty(inputUrl))
            url = inputUrl;

        websocket = new ClientWebSocket();
        Uri serverUri = new Uri(url);

        try
        {
            await websocket.ConnectAsync(serverUri, CancellationToken.None);
            Debug.Log("Connected to WebSocket server");

            // Start receiving messages
            _ = ReceiveMessages();

            // Start sending player position at regular intervals
            StartCoroutine(SendPlayerPositionAtIntervals());
        }
        catch (WebSocketException e)
        {
            Debug.LogError($"WebSocketException: {e.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception: {e.Message}");
        }
    }

    async Task SendMessage(string message)
    {
        var encodedMessage = Encoding.UTF8.GetBytes(message);
        var buffer = new ArraySegment<byte>(encodedMessage);

        try
        {
            await websocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            Debug.Log("Message sent: " + message);
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception while sending message: {e.Message}");
        }
    }

    async Task ReceiveMessages()
    {
        var buffer = new byte[1024];

        try
        {
            while (websocket.State == WebSocketState.Open)
            {
                var result = await websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Debug.Log("WebSocket closed by the server");
                    await websocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Debug.Log("Message received: " + message);
                    if (!isDisconnected) HandleIncomingMessage(message);
                }
            }
        }
        catch (WebSocketException e)
        {
            Debug.LogError($"WebSocketException while receiving messages: {e.Message}");
        }
        catch (OperationCanceledException e)
        {
            Debug.LogError($"OperationCanceledException while receiving messages: {e.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception while receiving messages: {e.Message}");
        }
    }

    private void HandleIncomingMessage(string message)
    {
        try
        {
            // Parse the incoming JSON message
            var playersData = JsonUtility.FromJson<PlayersData>(message);

            // Iterate through each player data in the JSON
            foreach (var playerData in playersData.players)
            {
                string playerEnemyId = playerData.id;
                if (playerId != playerEnemyId)
                {
                    Vector3 position = new Vector3(playerData.content.x, playerData.content.y, playerData.content.z);

                    // Check if the player with this id already exists
                    GameObject playerObject = GameObject.Find(playerEnemyId);

                    if (playerObject == null)
                    {
                        // If the player does not exist, instantiate a new player
                        GameObject newPlayer = Instantiate(playerEnemyPrefab, position, Quaternion.identity);
                        newPlayer.name = playerEnemyId; // Set the player's name to their id
                        newPlayer.GetComponent<PlayerName>().UpdateName(playerData.content.name);

                        leaderboardManager.AddOrUpdateEntry(newPlayer);
                    }
                    else
                    {
                        // If the player exists, update its position
                        playerObject.transform.position = position;
                        playerObject.GetComponent<PlayerStats>().currExp = playerData.content.exp;

                        leaderboardManager.AddOrUpdateEntry(playerObject);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception while handling incoming messages: {e.Message}");
        }
    }



    private Vector3? ParsePosition(string xString, string yString, string zString)
    {
        float x = float.Parse(xString);
        float y = float.Parse(yString);
        float z = float.Parse(zString);

        return new Vector3(x, y, z);
    }

    private Vector3? ParsePosition(PlayerContent playerContent)
    {
        return new Vector3(playerContent.x, playerContent.y, playerContent.z);
    }

    public async void SendPlayerPosition()
    {
        if (player != null)
        {
            var position = player.transform.position;
            var messageObject = new PlayerData
            {
                id = playerId,
                content = new PlayerContent
                {
                    x = position.x,
                    y = position.y,
                    z = position.z,
                    name = playerName,
                    exp = playerStats.currExp,
                }
            };

            var jsonMessage = JsonUtility.ToJson(messageObject);

            await SendMessage(jsonMessage);
        }
    }



    private IEnumerator SendPlayerPositionAtIntervals()
    {
        while (!isDisconnected)
        {
            SendPlayerPosition();
            yield return new WaitForSeconds(sendInterval);
        }
    }

    private void OnApplicationQuit()
    {
        Abort();
    }

    public async void Abort()
    {
        isDisconnected = true;

        if (websocket != null)
        {
            try
            {
                if (websocket.State == WebSocketState.Open)
                {
                    await websocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnect", CancellationToken.None);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception while closing WebSocket: {e.Message}");
            }
            finally
            {
                websocket.Dispose();
            }
        }
    }


    [Serializable]
    public class PlayersData
    {
        public PlayerData[] players;
    }

    [Serializable]
    public class PlayerData
    {
        public string id;
        public PlayerContent content;
    }

    [Serializable]
    public class PlayerContent
    {
        public float x;
        public float y;
        public float z;
        public string name;
        public int exp;
    }

}
