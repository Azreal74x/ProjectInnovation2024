using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro; // Make sure to include the TMPro namespace for access to TMP_InputField

public class PcSender : MonoBehaviour {

  public int port = 7087;
  public string messageToSend = null;
  public TMP_InputField text;
  private UdpClient client = new UdpClient();
  private string targetIP = null;

    private void Start()
    {
        /*Debug.Log( client); //  
        Debug.Log(client.Client); //  
        Debug.Log(client.Client.LocalEndPoint); //  
        Debug.Log(((IPEndPoint) client.Client.LocalEndPoint).Port); //  */
    }

    public void SetIP(string ip) {
    targetIP = text.text;
    SendIP();
  }

  private void SendToTarget(byte[] packet) {
    if (!string.IsNullOrEmpty(targetIP)) { // Make sure the target IP is not null or empty
      client.Send(packet, packet.Length, targetIP, port);
    }
  }

  private void Update() {
    if (!string.IsNullOrEmpty(targetIP)) { // ensure IP is set before sending
  
    }
  }

  private void SendIP() {
    if (!string.IsNullOrEmpty(targetIP)) {
      string message = "IP:" + targetIP;
      byte[] bytes = Encoding.ASCII.GetBytes(message);
      SendToTarget(bytes);
    }
  }

  public void SendUsedItem(string itemToSend) {
    if (!string.IsNullOrEmpty(itemToSend) && !string.IsNullOrEmpty(targetIP)) {
      itemToSend = "ITEM:" + itemToSend;
      byte[] bytes = Encoding.ASCII.GetBytes(itemToSend);
      SendToTarget(bytes);
    }
  }
}