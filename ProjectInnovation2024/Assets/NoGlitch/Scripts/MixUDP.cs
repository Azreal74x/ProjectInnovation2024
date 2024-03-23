using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MixUDP : MonoBehaviour
{

    [SerializeField] private string item = "PotionSecond"; //has to be name of the item in the inventory

    [SerializeField] private GameObject senderListener;
    private PcListener pcListener; //cache component

    private SpriteRenderer currentSprite;

    [SerializeField] private Sprite firstForm;
    [SerializeField] private Sprite secondForm;

    private bool finishedPouring = false;
    private int swingCount = 0;
    public bool check = false;

    private void Start()
    {
        if (senderListener != null)
        {
            pcListener = senderListener.GetComponent<PcListener>();
        }
        currentSprite = GetComponent<SpriteRenderer>();
        currentSprite.sprite = firstForm;
    }

    private void Update()
    {
        /*if (!finishedPouring)
        {
            return;
        }
        Debug.Log("finished pourning");
        */
        if (pcListener.accelerationSqrMagnitude > 20f)
        {
            swingCount++;

        }
        if (!check && swingCount >= 20)
        {
            currentSprite.sprite = secondForm;
            check = true;
        }

        

    }

   /* public void FinishedPouring()
    {
        currentSprite = redBeaker;
        finishedPouring = true;
    }*/
}
