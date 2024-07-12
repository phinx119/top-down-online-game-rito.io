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
    public GameObject player;
    public GameObject clone;
    public float sendInterval = 0.1f; // Interval in seconds

    async void Start()
    {
        string inputUrl = MainMenu.serverUrl;
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
                    HandleIncomingMessage(message);
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
        // Assuming the message format is "x,y,z"
        var position = ParsePosition(message);
        if (position.HasValue)
        {
            clone.transform.position = position.Value;
            Debug.Log("Clone position updated: " + position.Value);
        }
        else
        {
            Debug.LogError("Failed to parse position message: " + message);
        }
    }

    private Vector3? ParsePosition(string message)
    {
        var parts = message.Split(',');
        if (parts.Length == 3 &&
            float.TryParse(parts[0], out var x) &&
            float.TryParse(parts[1], out var y) &&
            float.TryParse(parts[2], out var z))
        {
            return new Vector3(x, y, z);
        }
        return null;
    }

    public async void SendPlayerPosition()
    {
        if (player != null)
        {
            var position = player.transform.position;
            var message = $"{position.x},{position.y},{position.z}";
            await SendMessage(message);
        }
    }

    private IEnumerator SendPlayerPositionAtIntervals()
    {
        while (true)
        {
            SendPlayerPosition();
            yield return new WaitForSeconds(sendInterval);
        }
    }

    private void OnApplicationQuit()
    {
        if (websocket != null)
        {
            websocket.Abort();
        }
    }
}
