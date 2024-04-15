using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using FMODUnity;

public class MixUDP : MonoBehaviour
{
    [SerializeField] private GameObject senderListener;
    private PcListener pcListener; //cache component
    private PcSender pcSender; //cache component

    [SerializeField] private string sulfericAcidItem = "SulfericAcid"; //has to be name of the item in the inventory
    [SerializeField] private string emptyItem = "Empty"; //has to be name of the item in the inventory

    private SpriteRenderer currentSprite;

    [SerializeField] private Sprite firstForm;
    [SerializeField] private Sprite secondForm;

    [SerializeField] private GameObject pourObj;

    private bool finishedPouring = false;
    private int swingCount = 0;
    public bool check = false;

    private FMOD.Studio.EventInstance shakeSound;
    [FMODUnity.EventRef][SerializeField] private string fmodShakeSound;

    private void Start()
    {
        pourObj.SetActive(false);


        if (senderListener != null)
        {
            pcListener = senderListener.GetComponent<PcListener>();
            pcSender = senderListener.GetComponent<PcSender>();
        }
        currentSprite = GetComponent<SpriteRenderer>();
        currentSprite.sprite = firstForm;

        shakeSound = FMODUnity.RuntimeManager.CreateInstance(fmodShakeSound);
        
    }

    private void Update()
    {


        if (pcListener.currentItem == "Empty" && pcListener.accelerationSqrMagnitude > 20f)
        {
            swingCount++;

        }
        if (!check && swingCount >= 20)
        {
            currentSprite.sprite = secondForm;
            check = true;
            shakeSound.start();

            pcSender.SendUsedItem(emptyItem);
            pcSender.SendItem(sulfericAcidItem);
        }
    }

    /* public void FinishedPouring()
     {
         currentSprite = redBeaker;
         finishedPouring = true;
     }*/
}
