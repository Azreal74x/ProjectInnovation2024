using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour {
  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component

  [SerializeField] private GameObject calibrationScreen;

  public Quaternion initialOrientation;
  public bool isCalibrated = false;
  public bool iphone = false;
  private bool choseVersion = false;

  private void Start() {
    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
    }

    pcListener = senderListener.GetComponent<PcListener>();
  }

  private void Update() {
    
  }

  public void IsIphone() {
    iphone = true;
    choseVersion = true;
  }

  public void IsAndroid() {
    iphone = false;
    choseVersion = true;
  }

  public void CalibrateGyro() {
    Quaternion currentGyroData = pcListener.gyroQuaternion; //get current phone gyro input from pcListener
    initialOrientation = Quaternion.Inverse(currentGyroData); //inverse it and set the initialOrientation for further use
    isCalibrated = true; //confirm calibration
    calibrationScreen.SetActive(false);
  }

  public void TurnOnCalibration() {
    calibrationScreen.SetActive(true);
  }
}
