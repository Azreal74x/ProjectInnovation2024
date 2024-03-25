using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowUDP : MonoBehaviour
{

    [SerializeField] private GameObject senderListener;
    private PcListener pcListener; //cache component
    private PcSender pcSender; //cache component
    
    [SerializeField] private string sulfericAcidItem = "SulfericAcid"; //has to be name of the item in the inventory

    [SerializeField] private GameObject sideRoomButton;
    public bool explosion = false;

    [SerializeField] private GameObject ExplodedDoor;

    void Start()
    {
        if (senderListener != null)
        {
            pcListener = senderListener.GetComponent<PcListener>();
            pcSender = senderListener.GetComponent<PcSender>();
        }
    }

    void Update()
    {
        if (pcListener.currentItem == sulfericAcidItem && pcListener.accelerationSqrMagnitude > 30f)
        {
            //Explosion();
            SwitchDoor();
        }
    }

    /*private void Explosion()
    {
        sideRoomButton.SetActive(true);
        explosion = true;
    }*/

    private void SwitchDoor()
    {
        ExplodedDoor.SetActive(true);
        sideRoomButton.SetActive(true);

        //Debug.Log("potion thrown succesfully");
        pcSender.SendUsedItem(sulfericAcidItem);
    }
}
