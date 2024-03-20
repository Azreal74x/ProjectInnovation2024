using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GetGyroData : MonoBehaviour
{

    [SerializeField] private GameObject senderListener;

    [SerializeField] private TextMeshProUGUI textBox; //for testing

    private Quaternion initialOrientation;


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
    }

    private void Update()
    {

        GyroCheck();
    }

    

    private void GyroCheck()
    {

        Vector3 gyroRot = Input.gyro.attitude.eulerAngles; 

       
        Vector3 spriteRotation = new Vector3(gyroRot.x, gyroRot.z, -gyroRot.y); 
        transform.rotation = Quaternion.Euler(spriteRotation);

        textBox.text = ConvertToMessage(spriteRotation);
    //Debug.Log(ConvertToMessage(spriteRotation));

        PhoneSender sender = senderListener.GetComponent<PhoneSender>();

        sender.messageToSend = ConvertToMessage(spriteRotation);

        //textBox.text = spriteRotation.ToString();
    }

    private string ConvertToMessage(Vector3 pGyroData) {
        Vector3 gyroData = pGyroData;

        string message = $"{gyroData.x},{gyroData.y},{gyroData.z}";

        return message;

    }

    
}
