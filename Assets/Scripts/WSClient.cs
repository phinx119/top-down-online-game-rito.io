using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WSClient : MonoBehaviour
{
    private ClientWebSocket websocket;
    public string url = "wss://websocket-server-kutx.onrender.com";

    async void Start()
    {
        websocket = new ClientWebSocket();
        Uri serverUri = new Uri(url);

        try
        {
            await websocket.ConnectAsync(serverUri, CancellationToken.None);
            Debug.Log("Connected to WebSocket server");

            // Start receiving messages
            await ReceiveMessages();
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

    private void OnApplicationQuit()
    {
        if (websocket != null)
        {
            websocket.Abort();
        }
    }
}
