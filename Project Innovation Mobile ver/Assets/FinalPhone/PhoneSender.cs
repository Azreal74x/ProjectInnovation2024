using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using TMPro;

public class PhoneSender : MonoBehaviour {
  //MOBILE

  //private const int port = 55551;
  public string messageToSend = null;
  public TMP_InputField text;
  private UdpClient client = new UdpClient(); //put here the port ?
  private string targetIP = null;

  private void Start() {
    client = new UdpClient(55551);

    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
    }
    else {
      //Debug.Log("Gyroscope not supported"); //message if not gyroscope supported
    }

  }

  public void SetIP(string ip) {
    targetIP = text.text;
    SendIP();
  }

  private void SendToTarget(byte[] packet) {
    if (!string.IsNullOrEmpty(targetIP)) { // Make sure the target IP is not null or empty
      client.Send(packet, packet.Length, targetIP, ((IPEndPoint)client.Client.LocalEndPoint).Port);
    }
  }

  private void Update() {
    if (!string.IsNullOrEmpty(targetIP)) { // ensure IP is set before sending
      SendGyroData();
      SendAccelerationData();
    }
  }

  private void SendIP() {
    if (!string.IsNullOrEmpty(targetIP)) {
      string message = "IP:" + targetIP; 
      byte[] bytes = Encoding.ASCII.GetBytes(message);
      SendToTarget(bytes);
      Debug.Log("IP SENT TO PC");
    }
  }

  private void SendGyroData() { 
    if (Input.gyro.enabled) {
      Quaternion gyroAttitude = Input.gyro.attitude;
      string gyroData = "GYRO:" + QuaternionToString(gyroAttitude);
      byte[] bytes = Encoding.ASCII.GetBytes(gyroData);
      SendToTarget(bytes);
    }
  }

  private void SendAccelerationData() {
    if (Input.gyro.enabled) {
      Vector3 acceleration = Input.acceleration;
      float sqrMagnitude = acceleration.sqrMagnitude; // Calculate squared magnitude
      string accelerationData = "ACCEL:" + sqrMagnitude.ToString();
      byte[] bytes = Encoding.ASCII.GetBytes(accelerationData);
      SendToTarget(bytes);
    }
  }
  string QuaternionToString(Quaternion q) {
    return $"{q.x},{q.y},{q.z},{q.w}";
  }

}