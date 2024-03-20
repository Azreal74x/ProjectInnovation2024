using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PourPotion : MonoBehaviour
{

    [SerializeField] private bool iphone = false;

    private float minPhoneRotationX;
    private float maxPhoneRotationX;
    private float minPourAngleY;
    private float maxPourAngleY;

    private Quaternion initialOrientation;
    private bool isCalibrated = false;

    private Vector3 spriteRotation;
    private Quaternion targetRotation;
    [SerializeField] private float rotationSpeed = 2f;

    [SerializeField] TextMeshProUGUI textBox;

    [SerializeField] Sprite purpleBeaker;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        { //check if device has gyroscope
            Input.gyro.enabled = true; //enable use of gyroscope
        }
        else
        {
            Debug.Log("Gyroscope not supported"); //message if not supported
        }
        targetRotation = transform.rotation;

        if (iphone)
        {
            minPhoneRotationX = 330;
            maxPhoneRotationX = 30;
        }
        else
        {
            minPhoneRotationX = 350;
            maxPhoneRotationX = 10;
        }
    }

    private void Update()
    {
        if (!isCalibrated)
        {
            return;
        }

        GyroCheck();
    }

    private void CalibrateGyro()
    {
        initialOrientation = Quaternion.Inverse(Input.gyro.attitude);
        isCalibrated = true;
    }

    public void Recalibrate()
    { //a method to recalibrate if needed
        CalibrateGyro();
    }

    private void GyroCheck()
    {
        // Apply the calibration offset to the current orientation
        Quaternion correctedOrientation = Input.gyro.attitude * initialOrientation;
        Vector3 gyroRotation = correctedOrientation.eulerAngles; // Use corrected orientation

        if (iphone)
        {
            if (gyroRotation.x > minPhoneRotationX || gyroRotation.x < maxPhoneRotationX)
            { //check we are pouring within the phone upright position within the x range
                Vector3 spriteRotation = new Vector3(0, 0, -gyroRotation.y);  // IPHONE
                targetRotation = Quaternion.Euler(spriteRotation); //easing
                if (gyroRotation.y > minPourAngleY && gyroRotation.y < maxPourAngleY) //check if we are pouring correct direction
                {
                    Pour();
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); //easing pt.2
            }
        }
        else
        {
            spriteRotation = new Vector3(0, 0, gyroRotation.y);  // ANDROID without if, otherwise -y
            targetRotation = Quaternion.Euler(spriteRotation); //easing
            if (gyroRotation.y > minPourAngleY && gyroRotation.y < maxPourAngleY) //check if we are pouring correct direction
            {
                Pour();
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); //easing pt.2
        }

        textBox.text = gyroRotation.ToString();
    }

    private void Pour()
    {
        Debug.Log("Pouring now");
    }


    private void TurnOff()
    {
        this.gameObject.SetActive(false);

    }

    public void TurnOn()
    {
        this.gameObject.SetActive(true);

    }


}
