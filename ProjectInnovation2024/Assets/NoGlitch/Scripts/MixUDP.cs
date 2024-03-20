using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MixUDP : MonoBehaviour
{

  [SerializeField] private string item = "PotionSecond"; //has to be name of the item in the inventory

  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component

    [SerializeField] private Sprite redBeaker;
    [SerializeField] private Sprite purpleBeaker;

    private bool finishedPouring = false;
    private int swingCount = 0;
    public bool check = false;

    private void Start()
    {
        if (senderListener != null)
        {
            pcListener = senderListener.GetComponent<PcListener>();
        }
    }

    private void Update()
    {
        if (!finishedPouring)
        {
            return;
        }
        Debug.Log("finished pourning");
        /*if (this.gameObject.GetComponent<PourPotionUDP>() != null)
        {*/
        if (pcListener.accelerationSqrMagnitude > 20f)
        {
            swingCount++;

        }
        if (!check && swingCount >= 20)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = purpleBeaker;
            check = true;
        }

        //}

    }

    public void FinishedPouring()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = redBeaker;
        finishedPouring = true;
    }
}
