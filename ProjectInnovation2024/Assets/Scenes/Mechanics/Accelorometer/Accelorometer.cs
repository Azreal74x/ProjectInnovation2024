using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp.Net;

public class Accelorometer : MonoBehaviour
{

    [SerializeField] Sprite purpleBeaker;
    private int swingCount = 0;
    private bool check = false;



    // Update is called once per frame
    void Update()
    {

        if(Input.acceleration.sqrMagnitude > 20f)
        {
            Debug.Log("shakey shake");
            swingCount++;

        }
        if (!check && swingCount >= 20)
        {
            Debug.Log("shook");
            this.gameObject.GetComponent<SpriteRenderer>().sprite = purpleBeaker;
            check = true;
        }
    }

}
