using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FMODUnity;

public class Calibration : MonoBehaviour
{
    [SerializeField] private GameObject senderListener;
    private PcListener pcListener; //cache component

    [SerializeField] private GameObject calibrationScreen;

    public Quaternion initialOrientation;
    public bool isCalibrated = false;
    public bool iphone = false;
    private bool choseVersion = false;

    private FMOD.Studio.EventInstance buttonClickSound;
    [FMODUnity.EventRef][SerializeField] private string fmodButtonClickSound;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        { //check if device has gyroscope
            Input.gyro.enabled = true; //enable use of gyroscope
        }

        pcListener = senderListener.GetComponent<PcListener>();

        buttonClickSound = FMODUnity.RuntimeManager.CreateInstance(fmodButtonClickSound);
    }

    private void Update()
    {
        //Debug.Log("from phone: " + pcListener.gyroQuaternion + " \n initial rotation: " + initialOrientation);
    }

    public void IsIphone()
    {
        iphone = true;
        choseVersion = true;
    }

    public void IsAndroid()
    {
        iphone = false;
        choseVersion = true;
    }

    public void CalibrateGyro()
    {
        Quaternion currentGyroData = pcListener.gyroQuaternion; //get current phone gyro input from pcListener
        initialOrientation = Quaternion.Inverse(currentGyroData); //inverse it and set the initialOrientation for further use
        isCalibrated = true; //confirm calibration
        calibrationScreen.SetActive(false);
        //Debug.Log("Calibrated GyroScope");
    }

    public void TurnOnCalibration()
    {
        calibrationScreen.SetActive(true);
        buttonClickSound.start();
    }
}
