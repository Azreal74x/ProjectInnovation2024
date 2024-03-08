using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using TMPro;

public class PhoneSender : MonoBehaviour {
  //MOBILE

  private const int port = 8089;
  public string messageToSend = null;
  public TMP_InputField text;
  private UdpClient client = new UdpClient();
  private string targetIP = null;

  private void Start() {
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
      client.Send(packet, packet.Length, targetIP, port);
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
    Vector3 acceleration = Input.acceleration;
    float sqrMagnitude = acceleration.sqrMagnitude; // Calculate squared magnitude
    string accelerationData = "ACCEL:" + sqrMagnitude.ToString();
    byte[] bytes = Encoding.ASCII.GetBytes(accelerationData);
    SendToTarget(bytes);
  }
  string QuaternionToString(Quaternion q) {
    return $"{q.x},{q.y},{q.z},{q.w}";
  }

}