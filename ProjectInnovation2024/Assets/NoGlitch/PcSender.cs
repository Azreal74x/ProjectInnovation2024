using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro; 

public class PcSender : MonoBehaviour {

  public string messageToSend = null;
  public TMP_InputField text;
  private UdpClient client;
  private string targetIP = null;

  public bool targetIPSet = false;

  private void Start() {
    client = new UdpClient(55550);
    /*Debug.Log( client); //  
    Debug.Log(client.Client); //  
    Debug.Log(client.Client.LocalEndPoint); //  
    Debug.Log(((IPEndPoint) client.Client.LocalEndPoint).Port); //  */
  }

  public void SetIP(string ip) {
    targetIP = text.text;
    targetIPSet = true;
    SendIP();
  }

  private void SendToTarget(byte[] packet) {
    if (!string.IsNullOrEmpty(targetIP)) { // Make sure the target IP is not null or empty
      client.Send(packet, packet.Length, targetIP, ((IPEndPoint)client.Client.LocalEndPoint).Port);
    }
  }

  private void Update() {
    if (!string.IsNullOrEmpty(targetIP)) { // ensure IP is set before sending

    }
  }

  private void SendIP() {
    if (!string.IsNullOrEmpty(targetIP)) {
      string message = "PC_IP:" + targetIP;
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