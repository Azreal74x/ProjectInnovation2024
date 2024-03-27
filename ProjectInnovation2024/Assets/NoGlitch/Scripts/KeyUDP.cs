using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using FMODUnity; // Import the FMODUnity namespace

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

    private bool key = false;
    private bool didBoom = false;

    [SerializeField] private GameObject doorEnd;

    public bool gameEnd = false;

    [SerializeField] private GameObject endComic;


    private FMOD.Studio.EventInstance pickupSound;
    [FMODUnity.EventRef][SerializeField] private string fmodPickupSound;

    private FMOD.Studio.EventInstance endMusic;
    [FMODUnity.EventRef][SerializeField] private string fmodEndMusic;

    private FMOD.Studio.EventInstance openMetalDoor;
    [FMODUnity.EventRef][SerializeField] private string fmodOpenMetalDoor;


    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        { //check if device has gyroscope
            Input.gyro.enabled = true; //enable use of gyroscope
        }
        else
        {
            //Debug.Log("Gyroscope not supported"); //message if not supported
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

        pickupSound = FMODUnity.RuntimeManager.CreateInstance(fmodPickupSound);
        endMusic = FMODUnity.RuntimeManager.CreateInstance(fmodEndMusic);
        openMetalDoor = FMODUnity.RuntimeManager.CreateInstance(fmodOpenMetalDoor);
    }


    private void Update()
    {
        if (!calibration.isCalibrated)
        {
            return;
        }
        CheckPhone();

        //Debug.Log("key? " + key);

        if (key && pcListener.currentItem == "Key")
        {
            GyroCheck();
        }
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

                OpenDoor();
            }
        }

        /*if (easing)
        {
            targetRotation = Quaternion.Euler(spriteRotation); //easing
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Euler(spriteRotation); //set the current sprite rotation to the vector with Euler to avoid gimball lock
        }*/

        //Debug.Log(gyroRot); //show text
    }

    private void OpenDoor()
    {
        if (!didBoom)
        {
            //Debug.Log("Boom");

            doorEnd.SetActive(true);

            pcSender.SendUsedItem(keyItem);

            gameEnd = true;
            StartCoroutine(WaitSomeSec());

            openMetalDoor.start();
        }
        didBoom = true;

    }


    private IEnumerator WaitSomeSec()
    {
        yield return new WaitForSeconds(2);

        endComic.SetActive(true);
        endMusic.start();   //THESE WILL PLAY AND LOOP EVERY FRAME. WHEN YOU HAVE A WAY TO DETECT THE GAME HAS STARTED (when we play the normal music) BE SURE TO PUTT IN A

        Debug.Log("waited 2 secs");
        StartCoroutine(WaitMoreSec());

    }

    private IEnumerator WaitMoreSec()
    {
        yield return new WaitForSeconds(5);

        Debug.Log("waited 5 sec");

        endMusic.release();

        SceneManager.LoadScene(0);
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
        pickupSound.start();

    }

}
