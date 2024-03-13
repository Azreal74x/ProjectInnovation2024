/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GetGyroData : MonoBehaviour
{

    [SerializeField] private GameObject senderListener;

    [SerializeField] private TextMeshProUGUI hiddenImage; //for testing

    private Vector3 gyroData;
    private string messageReceived;

    private Quaternion initialOrientation;


    private void Start()
    {
        *//*
         if (SystemInfo.supportsGyroscope)
        { //check if device has gyroscope
            Input.gyro.enabled = true; //enable use of gyroscope
        }
        else
        {
            Debug.Log("Gyroscope not supported"); //message if not supported
        }
        *//*
    }

    private void Update()
    {

        GyroCheck();
    }



    private void GyroCheck()
    {

        UDPListener listener = senderListener.GetComponent<UDPListener>();
        this.messageReceived = listener.messageReceived;

        hiddenImage.text = messageReceived;

        string[] gyroDataParts = messageReceived.Split(',');
        Debug.Log(gyroDataParts);
        if (gyroDataParts.Length == 3)
        {
            gyroData.x = float.Parse(gyroDataParts[0]);
            gyroData.y = float.Parse(gyroDataParts[1]);
            gyroData.z = float.Parse(gyroDataParts[2]);

            transform.rotation = Quaternion.Euler(gyroData);
        }


    }

    

    private string ConvertToMessage(Vector3 pGyroData)
    {
        Vector3 gyroData = pGyroData;

        string message = $"{gyroData.x},{gyroData.y},{gyroData.z}";

        return message;

    }


}
*/