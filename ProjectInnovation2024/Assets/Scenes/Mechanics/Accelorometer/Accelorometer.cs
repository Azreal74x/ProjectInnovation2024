using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Accelorometer : MonoBehaviour
{

    public Sprite redBeaker;
    public Sprite purpleBeaker;

    Vector3 accelerometer;


    // Update is called once per frame
    void Update()
    {
        accelerometer = Input.acceleration;

        if(accelerometer.sqrMagnitude > 20f)
        {
            Debug.Log("shakey shake");
            this.gameObject.GetComponent<SpriteRenderer>().sprite = purpleBeaker;

        }
    }

}
