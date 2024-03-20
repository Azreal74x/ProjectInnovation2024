using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine.Device;

public class PcListener : MonoBehaviour {

  //PC

  private UdpClient client;
  private IPEndPoint endPoint;

  public Quaternion gyroQuaternion;
  public float accelerationSqrMagnitude;

  [SerializeField] private TextMeshProUGUI ipv4;
  private string currentIP;

  [SerializeField] private GameObject screenIP;

  private PcSender pcSender;
  [SerializeField] private GameObject senderListener;
  private bool setIP = false;

  private void Start() {
    client = new UdpClient(55551);
    endPoint = new IPEndPoint(IPAddress.Any, 0);
    LogLocalIPAddress();

    pcSender = senderListener.GetComponent<PcSender>();
  }

  private void Update() {
    ReceiveData();

    if (setIP && pcSender.targetIPSet) {
      screenIP.SetActive(false);
    }
  }

  private void LogLocalIPAddress() {
    var host = Dns.GetHostEntry(Dns.GetHostName());
    foreach (var ip in host.AddressList) {
      if (ip.AddressFamily == AddressFamily.InterNetwork) {
        //Debug.Log("Local IPv4: " + ip.ToString());
        currentIP = ip.ToString();
        ipv4.text = ip.ToString();
        break; // Remove this break if you want to log all IPv4 addresses
      }
    }
  }

  void ReceiveData() {
    while (client.Available > 0) {
      byte[] inBytes = client.Receive(ref endPoint);
      string inString = Encoding.UTF8.GetString(inBytes);

      if (inString.StartsWith("PHONE_IP:")) {
        Debug.Log($"Received IP: {inString.Substring(9)} from {endPoint}");
        string receivedIP = inString.Substring(9).Trim();
        if (receivedIP == currentIP) {
          ipv4.text = " ";
          setIP = true;

        }
        else {
          Debug.Log("Try again, receivedIP is: " + receivedIP + " but the current IP is: " + currentIP);
          //ScreenIP.SetActive(true);
        }
      }
      else if (inString.StartsWith("GYRO:")) {
        string gyroData = inString.Substring(5);
        gyroQuaternion = StringToQuaternion(gyroData);
        //Debug.Log($"Received Gyro Data: {gyroData} from {endPoint}");
      }
      else if (inString.StartsWith("ACCEL:")) {
        string accelData = inString.Substring(6);
        accelerationSqrMagnitude = float.Parse(accelData);
        //Debug.Log($"Received Acceleration Squared Magnitude: {accelerationSqrMagnitude} from {endPoint}");
      }
    }
  }

  Quaternion StringToQuaternion(string s) {
    string[] values = s.Split(',');
    if (values.Length == 4) {
      return new Quaternion(
          float.Parse(values[0]),
          float.Parse(values[1]),
          float.Parse(values[2]),
          float.Parse(values[3]));
    }
    return Quaternion.identity;
  }

  private void OnDestroy() {
    if (client != null)
      client.Close();
  }
}
