using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;

public class PhoneListener : MonoBehaviour {

  private UdpClient client;
  private IPEndPoint endPoint;

  [SerializeField] private TextMeshProUGUI ipv4;
  private string currentIP;

  [SerializeField] private GameObject inventoryController;
  private Inventory inventory ; //cache component

  public bool setIP = false;

  private void Start() {
    client = new UdpClient(8089);
    endPoint = new IPEndPoint(IPAddress.Any, 0);
    GetLocalIPAddress();

    inventory = inventoryController.GetComponent<Inventory>();
  }

  // Update is called once per frame
  void Update() {
    ReceiveData();
  }

  void ReceiveData() {
    while (client.Available > 0) {
      byte[] inBytes = client.Receive(ref endPoint);
      string inString = Encoding.UTF8.GetString(inBytes);

      if (inString.StartsWith("IP:")) {
        Debug.Log($"Received IP: {inString.Substring(3)} from {endPoint}");
        string receivedIP = inString.Substring(3).Trim();
        ipv4.text = receivedIP;
        if (receivedIP == currentIP) {
          ipv4.text = " ";
          setIP = true;
        }
        else {
          Debug.Log("Try again");
        }
      }
      else if (inString.StartsWith("ITEM:")) {
        string receivedItem = inString.Substring(5);
        if (receivedItem != null) {
          inventory.MarkItemAsUsed(receivedItem);
          Debug.Log($"Received Item: {receivedItem} from {endPoint}");
        }
      }
    }
  }

  // Method to get the local IP address
  private void GetLocalIPAddress() {
    var host = Dns.GetHostEntry(Dns.GetHostName());
    foreach (var ip in host.AddressList) {
      if (ip.AddressFamily == AddressFamily.InterNetwork) {
        currentIP = ip.ToString();
        ipv4.text = currentIP;
      }
    }
  }


  private void OnDestroy() {
    if (client != null)
      client.Close();
  }
}