using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public Quaternion initialOrientation;
    public bool isIphone;
    public bool isCalibrated;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        //Debug.Log("GameManager present");


    }


    void Update()
    {
        Debug.Log(initialOrientation);
    }


}
