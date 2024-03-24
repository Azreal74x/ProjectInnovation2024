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

  public bool targetIPSet = false;

  [SerializeField] private GameObject inventoryController;
  private Inventory inventory;
  private string lastSentItem = null;

  private void Start() {
    client = new UdpClient(55551);

    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
    }
    else {
      //Debug.Log("Gyroscope not supported"); //message if not gyroscope supported
    }

    if(inventoryController != null) {
      inventory = inventoryController.GetComponent<Inventory>();  
    }

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
      SendGyroData();
      SendAccelerationData();
      SendCurrentInventoryItem();
    }
  }

  private void SendIP() {
    if (!string.IsNullOrEmpty(targetIP)) {
      string message = "PHONE_IP:" + targetIP; 
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

  private void SendCurrentInventoryItem() {
    // Check if the current item has changed from the last sent item
    // This includes checking for a change to or from null
    if (inventory.currentItem != lastSentItem) {
      string message = "CURRENT ITEM:";
      if (!string.IsNullOrEmpty(inventory.currentItem)) {
        message += inventory.currentItem; // Add the current item name if not null
      }
      else {
        message += "null"; // Explicitly send "null" if no item is selected
      }
      byte[] bytes = Encoding.ASCII.GetBytes(message);
      SendToTarget(bytes);

      lastSentItem = inventory.currentItem; // Update the last sent item
    }
  }

  string QuaternionToString(Quaternion q) {
    return $"{q.x},{q.y},{q.z},{q.w}";
  }

}