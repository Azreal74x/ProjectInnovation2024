using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PourPotion : MonoBehaviour {

  [SerializeField] private float minPhoneRotationX = 30;
  [SerializeField] private float maxPhoneRotationX = 70;
  [SerializeField] private float minPourAngleX = 120;
  [SerializeField] private float maxPourAngleX = 180;

  [SerializeField] private TextMeshProUGUI textBox; //for testing
  [SerializeField] private bool upright = true; //for testing

  private void Start() {
    if (SystemInfo.supportsGyroscope) { //check if device has gyroscope
      Input.gyro.enabled = true; //enable use of gyroscope
    }
    else {
      Debug.Log("Gyroscope not supported"); //message if not supported
    }
  }

  private void Update() {
    GyroCheck();
  }

  private void GyroCheck() {
    Vector3 gyroRot = new Vector3(Input.gyro.attitude.eulerAngles.x, Input.gyro.attitude.eulerAngles.y, Input.gyro.attitude.eulerAngles.z); //set gyro input to vector3
    Vector3 spriteRotation = new Vector3(0, 0, gyroRot.z); //set sprite rotationZ to gyroZ
    if (gyroRot.x > minPhoneRotationX && gyroRot.x < maxPhoneRotationX) { //check if phone is upright within minimum and maximum values
      transform.rotation = Quaternion.Euler(spriteRotation); //set sprite rotationZ to vector3 spriteRotation
      if (gyroRot.z > minPourAngleX && gyroRot.z < maxPourAngleX) { //check if phone is doing pouring motion within minimum and maximum values
        Pour(); 
      }
      else {
        textBox.text = "Now pour the potion";
      }
    }
    else {
      if (upright) textBox.text = "Hold your phone upright";
    }
  }

  private void Pour() {
    textBox.text = "Good job!";
    Debug.Log("Pouring now");
  }
}
