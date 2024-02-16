using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVLight : MonoBehaviour {

  [SerializeField] private float minPhoneRotationX = 180; //maximum up when faced away
  [SerializeField] private float maxPhoneRotationX = 320; //minimum left when faced away
  [SerializeField] private float minUVAngleY = 60; //minimum down when faced away
  [SerializeField] private float maxUVAngleY = 255; //maximum right when faced away

  [SerializeField] private GameObject textBox;

  private void Start() {

  }

  private void Update() {
    GyroCheck();
  }

  private void GyroCheck() {
    Vector3 gyroRot = new Vector3(Input.gyro.attitude.eulerAngles.x, Input.gyro.attitude.eulerAngles.y, Input.gyro.attitude.eulerAngles.z); //set gyro input to vector3
    if (gyroRot.x > minPhoneRotationX && gyroRot.x < maxPhoneRotationX && gyroRot.y > minUVAngleY && gyroRot.y < maxUVAngleY) { //check gyro x and y rotation if screen is positioned away from player
      textBox.SetActive(true); //show secret text
    }
    else {
      textBox.SetActive(false); //hide secret text if wrong gyro phone rotation
    }
  }
}
