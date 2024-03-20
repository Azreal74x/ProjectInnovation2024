using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourPotionUDP : MonoBehaviour {

  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component
  private PcSender pcSender; //cache component

  [SerializeField] private string item = "Water"; //has to be name of the item in the inventory
  [SerializeField] private string secondItem = "Sulfer"; //has to be name of the item in the inventory
  
  public bool newPotion = false; //WE HAVE TO DO THIS

  private Quaternion previousGyroData;

  private float minPhoneRotationX;
  private float maxPhoneRotationX;
  private float minPourAngleY;
  private float maxPourAngleY;

  [SerializeField] private GameObject calibrationController;
  private Calibration calibration;

  private Vector3 spriteRotation;
  private Quaternion targetRotation;
  [SerializeField] private float rotationSpeed = 5f;

  [SerializeField] private GameObject vessel;

  private bool didPour = false;


  void Start() {
    if (senderListener != null) {
      pcListener = senderListener.GetComponent<PcListener>();
      pcSender = senderListener.GetComponent<PcSender>();
    }

    if (calibrationController != null) {
      calibration = calibrationController.GetComponent<Calibration>();
    }

    targetRotation = transform.rotation;

  }


  private void Update() {
    if (!calibration.isCalibrated) {
      return;
    }

    Debug.Log("is calibrated");
    CheckPhone();
    GyroCheck();


  }
  private void GyroCheck() {
    Quaternion currentGyroData = pcListener.gyroQuaternion;
    if (currentGyroData != previousGyroData) { //only update when the value changes

      Quaternion correctedOrientation = currentGyroData * calibration.initialOrientation; //apply the calibration offset to the current orientation
            // initial orientation on the left if u wanna ctually invert it
      Vector3 gyroRotation = correctedOrientation.eulerAngles; // Use corrected orientation
            //Quaternion.angle !!!
      if (calibration.iphone) {
        if (gyroRotation.x > minPhoneRotationX || gyroRotation.x < maxPhoneRotationX) { //check we are pouring within the phone upright position within the x range
          Vector3 spriteRotation = new Vector3(0, 0, -gyroRotation.y);  // IPHONE
          targetRotation = Quaternion.Euler(spriteRotation); //easing
          if (gyroRotation.y > minPourAngleY && gyroRotation.y < maxPourAngleY) //check if we are pouring correct direction
          {
            Pour();
          }
          transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); //easing pt.2
        }
      }
      else {
        spriteRotation = new Vector3(0, 0, -gyroRotation.y);  // ANDROID without x if, otherwise -y
        targetRotation = Quaternion.Euler(spriteRotation); //easing
        if (gyroRotation.y > minPourAngleY && gyroRotation.y < maxPourAngleY) //check if we are pouring correct direction
        {
          Pour();
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); //easing pt.2
      }

      previousGyroData = currentGyroData; //update previous data
    }
  }

  private void Pour() {
    if (!didPour) {
      //Debug.Log("Pouring now");
      pcSender.SendUsedItem(item);
    }
    didPour = true;
    StartCoroutine(WaitSomeSecs());
  }


  private IEnumerator WaitSomeSecs() {
    yield return new WaitForSeconds(2);
    vessel.SetActive(true);
    //vessel.GetComponent<MixUDP>().FinishedPouring();
  }

  private void CheckPhone() {
    if (calibration.iphone) {
      minPhoneRotationX = 330;
      maxPhoneRotationX = 30;
      minPourAngleY = 180;
      maxPourAngleY = 240;
    }
    else {
      minPhoneRotationX = 350;
      maxPhoneRotationX = 10;
      minPourAngleY = 120;
      maxPourAngleY = 180;
    }
  }
}
