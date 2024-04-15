using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using FMODUnity; // Import the FMODUnity namespace

public class UVLightUDP : MonoBehaviour
{

    [SerializeField] private GameObject senderListner;
    private PcListener pcListener; //cache component

    [SerializeField] private GameObject calibrationController;
    private Calibration calibration;

    private float minPhoneRotationX; //maximum up when faced away
    private float maxPhoneRotationX; //minimum left when faced away
    private float minUVAngleY; //minimum down when faced away
    private float maxUVAngleY; //maximum right when faced away

    private bool gotFlashlight = false;

    //testing stuff so the uv shows

    public SpriteRenderer currentSprite;

    [SerializeField] private Sprite uvPaper;
    [SerializeField] private Sprite secretText;

    private FMOD.Studio.EventInstance uvLightUse;
    [FMODUnity.EventRef][SerializeField] private string fmodUvLightUse;

    private void Start()
    {
        if (senderListner != null)
        {
            pcListener = senderListner.GetComponent<PcListener>();

        }

        if (calibrationController != null)
        {
            calibration = calibrationController.GetComponent<Calibration>();
        }

        currentSprite = GetComponent<SpriteRenderer>();

        uvLightUse = FMODUnity.RuntimeManager.CreateInstance(fmodUvLightUse);
    }

    private void Update()
    {
        if (!calibration.isCalibrated)
        {
            return;
        }
        CheckPhone();
        if (pcListener.currentItem == "UVLight")
        {
            uvLightUse.start();

            GyroCheck();
        }
        else
        {
            uvLightUse.release();
        }
    }

    private void GyroCheck()
    {
        Quaternion currentGyroData = pcListener.gyroQuaternion;
        Vector3 gyroRot = new Vector3(currentGyroData.eulerAngles.x, currentGyroData.eulerAngles.y, currentGyroData.eulerAngles.z); //set gyro input to vector3

        if (calibration.iphone && gyroRot.x > minPhoneRotationX && gyroRot.x < maxPhoneRotationX && gyroRot.y > minUVAngleY && gyroRot.y < maxUVAngleY)
        { //check gyro x and y rotation if screen is positioned away from player
            ShowText(); //show secret text

        }
        else if (!calibration.iphone && gyroRot.x < minPhoneRotationX && gyroRot.x > maxPhoneRotationX && gyroRot.y > minUVAngleY && gyroRot.y < maxUVAngleY)
        {
            ShowText(); //show secret text


        }
        else
        {

            currentSprite.sprite = uvPaper;

        }

    }

    private void ShowText()
    {

        currentSprite.sprite = secretText;
    }

    private void CheckPhone()
    {
        if (calibration.iphone)
        {
            minPhoneRotationX = 180;
            maxPhoneRotationX = 320;
            minUVAngleY = 60;
            maxUVAngleY = 255;
        }
        else
        {
            minPhoneRotationX = 340; //340
            maxPhoneRotationX = 120; //60
            minUVAngleY = 240;
            maxUVAngleY = 300;
        }
    }

    public void TurnOff()
    {
        this.gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        this.gameObject.SetActive(true);
    }

    public void GotFlashlight()
    {
        gotFlashlight = true;
    }



}
