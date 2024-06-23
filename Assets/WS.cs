using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WS : MonoBehaviour
{
    private ClientWebSocket _webSocket;

    public async Task ConnectAsync(string uri)
    {
        _webSocket = new ClientWebSocket();
        try
        {
            await _webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
            Debug.Log("Connected to WebSocket server");
            _ = ReceiveMessages(); // Start receiving messages
        }
        catch (Exception ex)
        {
            Debug.LogError("WebSocket connection error: " + ex.Message);
        }
    }

    public async Task SendMessageAsync(string message)
    {
        if (_webSocket != null && _webSocket.State == WebSocketState.Open)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            Debug.Log("Message sent: " + message);
        }
    }

    private async Task ReceiveMessages()
    {
        var buffer = new byte[1024];
        while (_webSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result;
            try
            {
                result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    Debug.Log("WebSocket connection closed");
                }
                else
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Debug.Log("Message received: " + message);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("WebSocket receive error: " + ex.Message);
            }
        }
    }

    public async Task CloseAsync()
    {
        if (_webSocket != null)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            _webSocket.Dispose();
            Debug.Log("WebSocket connection closed");
        }
    }

    private void OnApplicationQuit()
    {
        CloseAsync().Wait();
    }
}
