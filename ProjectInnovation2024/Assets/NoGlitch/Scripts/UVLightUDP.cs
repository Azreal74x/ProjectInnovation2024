using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class UVLightUDP : MonoBehaviour {
  [SerializeField] private string uvLightItem = "UVLight"; //has to be name of the item in the inventory

  [SerializeField] private GameObject senderListner;
  private PcListener pcListener; //cache component
  private PcSender pcSender; //cache component

  [SerializeField] private GameObject calibrationController;
  private Calibration calibration;

  [SerializeField] private float minPhoneRotationX = 180; //maximum up when faced away
  [SerializeField] private float maxPhoneRotationX = 320; //minimum left when faced away
  [SerializeField] private float minUVAngleY = 60; //minimum down when faced away
  [SerializeField] private float maxUVAngleY = 255; //maximum right when faced away

  private bool gotFlashlight = false;

  //testing stuff so the uv shows

  //[SerializeField] private GameObject paperObject;
  public SpriteRenderer currentSprite;

  [SerializeField] private Sprite uvPaper;
  [SerializeField] private Sprite secretText;
  //[SerializeField] private GameObject secretTextImg;

  //public bool discovered = false;


  private void Start() {
    if (senderListner != null) {
      pcListener = senderListner.GetComponent<PcListener>();

    }

    //Debug.Log(gameManager.isCalibrated);

    if (calibrationController != null) {
      calibration = calibrationController.GetComponent<Calibration>();
    }

    currentSprite = GetComponent<SpriteRenderer>();

  }

  private void Update() {
    if (!calibration.isCalibrated) {
      return;
    }
    CheckPhone();
    if (gotFlashlight) {
      GyroCheck();
    }
  }

  private void GyroCheck() {
    Quaternion currentGyroData = pcListener.gyroQuaternion;
    Vector3 gyroRot = new Vector3(currentGyroData.eulerAngles.x, currentGyroData.eulerAngles.y, currentGyroData.eulerAngles.z); //set gyro input to vector3

    if (calibration.iphone && gyroRot.x > minPhoneRotationX && gyroRot.x < maxPhoneRotationX && gyroRot.y > minUVAngleY && gyroRot.y < maxUVAngleY) { //check gyro x and y rotation if screen is positioned away from player
      ShowText(); //show secret text

    }
    else if (!calibration.iphone && gyroRot.x < minPhoneRotationX && gyroRot.x > maxPhoneRotationX && gyroRot.y > minUVAngleY && gyroRot.y < maxUVAngleY) {
      ShowText(); //show secret text


    }
    else {
      //hiddenImage.SetActive(false); //hide secret text if wrong gyro phone rotation
      //debugText.text = "hiding secret text";
      //Debug.Log("hiding secret text");
      currentSprite.sprite = uvPaper;

      //Debug.Log(currentSprite.name);
    }
  }

  private void ShowText() {
    //discovered = true;
    //debugText.text = "should show secret text";

    currentSprite.sprite = secretText;
    //Debug.Log("should show secret text");
  }

  private void CheckPhone() {
    if (calibration.iphone) {
      minPhoneRotationX = 180;
      maxPhoneRotationX = 320;
      minUVAngleY = 60;
      maxUVAngleY = 255;
    }
    else {
      minPhoneRotationX = 340;
      maxPhoneRotationX = 60;
      minUVAngleY = 240;
      maxUVAngleY = 300;
    }
  }

  public void TurnOff() {
    this.gameObject.SetActive(false);
  }

  public void TurnOn() {
    this.gameObject.SetActive(true);
  }

  public void GotFlashlight() {
    gotFlashlight = true;
    pcSender.SendItem(uvLightItem);
  }

}
