using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUDP : MonoBehaviour
{

    [SerializeField] private GameObject senderListener;
    private PcListener pcListener; //cache component
    private PcSender pcSender; //cache component

    [SerializeField] private string keyItem = "Key"; //has to be name of the item in the inventory

    [SerializeField] private GameObject calibrationController;
    private Calibration calibration;

    [SerializeField] private bool easing = false;
    [SerializeField] private float rotationSpeed = 2f;
    private Quaternion targetRotation;
    private Vector3 spriteRotation;

    [SerializeField] private float minPhoneRotationX;
    [SerializeField] private float maxPhoneRotationX;
    [SerializeField] private float minTurnAngleZ;
    [SerializeField] private float maxTurnAngleZ;

    [SerializeField] private GameObject door;
    [SerializeField] private Sprite doorOpen;

    private bool key = false;
    private bool didBoom = false;

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

        if (senderListener != null)
        {
            pcListener = senderListener.GetComponent<PcListener>();
            pcSender = senderListener.GetComponent<PcSender>();
        }

        if (calibrationController != null)
        {
            calibration = calibrationController.GetComponent<Calibration>();
        }

        if (easing)
        {
            targetRotation = transform.rotation;
        }
    }


    private void Update()
    {
        if (!calibration.isCalibrated)
        {
            return;
        }
        CheckPhone();

        //if (key) {
        GyroCheck();
        //}
        Debug.Log("key? " + key);
    }

    private void GyroCheck()
    {
        Quaternion currentGyroData = pcListener.gyroQuaternion;
        Quaternion correctedOrientation = currentGyroData * calibration.initialOrientation;
        Vector3 gyroRot = correctedOrientation.eulerAngles; // Use corrected orientation

        if (gyroRot.x > minPhoneRotationX && gyroRot.x < maxPhoneRotationX)
        { //check we are pouring within the phone upright position within the x range
            spriteRotation = new Vector3(0, 0, -gyroRot.z); //due to weird coordinate space we set the spriteRotation's Z to -y

            if (gyroRot.z > minTurnAngleZ && gyroRot.z < maxTurnAngleZ)
            { //check if we are pouring correct direction

                ExplosionAndOpenDoor();
            }
        }

        if (easing)
        {
            targetRotation = Quaternion.Euler(spriteRotation); //easing
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Euler(spriteRotation); //set the current sprite rotation to the vector with Euler to avoid gimball lock
        }

        //Debug.Log(gyroRot); //show text
    }

    private void ExplosionAndOpenDoor()
    {
        if (!didBoom)
        {
            Debug.Log("Boom");
            door.GetComponent<SpriteRenderer>().sprite = doorOpen;
            pcSender.SendUsedItem(keyItem);
        }
        didBoom = true;
    }

    private void CheckPhone()
    {
        if (calibration.iphone)
        {
            minPhoneRotationX = 270f;
            maxPhoneRotationX = 310f;
            minTurnAngleZ = 190f;
            maxTurnAngleZ = 200f;
        }
        else
        {
            minPhoneRotationX = 50f;
            maxPhoneRotationX = 90f;
            minTurnAngleZ = 170f;
            maxTurnAngleZ = 180f;
        }
    }

    public void KeyPicked()
    {
        key = true;
        pcSender.SendItem(keyItem);

    }

}
