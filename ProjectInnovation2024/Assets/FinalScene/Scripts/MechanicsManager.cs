using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using FMODUnity; // Import the FMODUnity namespace



public class MechanicsManager : MonoBehaviour
{
    [SerializeField] private GameObject senderListener;
    private PcSender pcSender; //cache component

    [SerializeField] private GameObject backButton;

    [SerializeField] private string keyItem = "Key"; //has to be name of the item in the inventory
    [SerializeField] private string waterItem = "Water"; //has to be name of the item in the inventory
    [SerializeField] private string uvLightItem = "UVLight"; //has to be name of the item in the inventory
    [SerializeField] private string noteItem = "Note"; //has to be name of the item in the inventory

    [SerializeField] private GameObject backgroundSwitchingController;
    private BackgroundSwitching backgroundSwitching;

    [SerializeField] private List<GameObject> pickups;

    private bool interactedWithUV = false;
    private bool sulfur = false;

    [SerializeField] private GameObject arrowLeft;
    [SerializeField] private GameObject arrowRight;

    [SerializeField] private GameObject potionToPour;


    [SerializeField] private GameObject roomBrewery;
    [SerializeField] private GameObject roomKey;
    [SerializeField] private GameObject roomDoor;
    [SerializeField] private GameObject endScreen;

    [SerializeField] private GameObject calibrationObj;

    [SerializeField] private GameObject keyGameObj;


    private FMOD.Studio.EventInstance pickupSound;
    [FMODUnity.EventRef][SerializeField] private string fmodPickupSound; // The name of the FMOD event you want to trigger

    private FMOD.Studio.EventInstance notePickupSound;
    [FMODUnity.EventRef][SerializeField] private string fmodNotePickupSound;


    private FMOD.Studio.EventInstance backgroundSound;
    [FMODUnity.EventRef][SerializeField] private string fmodBackgroundSound;

    private void Start()
    {
        if (senderListener != null)
        {
            pcSender = senderListener.GetComponent<PcSender>();
        }

        backgroundSwitching = backgroundSwitchingController.GetComponent<BackgroundSwitching>();

        pickupSound = FMODUnity.RuntimeManager.CreateInstance(fmodPickupSound); // Create an instance of the FMOD event made by sno :D (i don't do CamelCasing)
        notePickupSound = FMODUnity.RuntimeManager.CreateInstance(fmodNotePickupSound);
        backgroundSound = FMODUnity.RuntimeManager.CreateInstance(fmodBackgroundSound);

        backgroundSound.start();
    }

    private void Update()
    {
        //Debug.Log(currentScreen);

        //Debug.Log("hammer? " + pickups[1].GetComponentInChildren<HammerUDP>().hammer);

        SwitchMechanics(backgroundSwitching.currentScreen);

    }
    private void SwitchMechanics(int room)
    {

        if (room == 0)
        {
            pickups[0].SetActive(true); // 
           
        }
        else if (room != 0)
        {
            pickups[0].SetActive(false);
        }
        ////////////
        if (room == 1)
        {
            pickups[1].SetActive(true);

        }
        else if (room != 1)
        {
            pickups[1].SetActive(false);

        }
        ////////////
        if (room == 2)  //  room 2  pour & mix potion
        {
            pickups[2].SetActive(true);

        }
        else if (room != 2)
        {
            pickups[2].SetActive(false);
        }

        ////////////

        if (roomBrewery.activeSelf)  //      zoom in room 2 
        {
            pickups[2].SetActive(false);

            pickups[3].SetActive(true);

            if (sulfur) // if we took the potion
            {
                potionToPour.SetActive(true); // show the vessel wuth mix in it
            }

            if (gameObject.GetComponentInChildren<MixUDP>().enabled == true)
            {
                potionToPour.SetActive(false);
            }
        }
        else if (!roomBrewery.activeSelf)
        {


            pickups[3].SetActive(false);
        }
        //////////////
        if (roomKey.activeSelf)  //      zoom in room 2 
        {
            pickups[0].SetActive(false);

            pickups[4].SetActive(true);


        }
        else if (!roomBrewery.activeSelf)
        {
            pickups[4].SetActive(false);
        }

        if (roomDoor.activeSelf)
        {
            pickups[2].SetActive(false);

            pickups[5].SetActive(true);
        }
        else if (!roomBrewery.activeSelf)
        {
            pickups[5].SetActive(false);
        }


        if (endScreen.activeSelf || calibrationObj.activeSelf)
        {

            for (int i = 0; i < pickups.Count; i++)
            {
                pickups[i].SetActive(false);
            }

        }
        else if (endScreen.activeSelf)
        {
            backButton.SetActive(false);
        }


    }

    public void InteractedWithUV()
    {
        interactedWithUV = true;
    }

    public void GotSulfur()
    {
        sulfur = true;

        pickupSound.start(); // Start playing the FMOD event
    }

    public void GotWater()
    {
        pcSender.SendItem(waterItem);

        pickupSound.start(); // Start playing the FMOD event
    }

    public void GotUVFlashlight()
    {
        pcSender.SendItem(uvLightItem);

        pickupSound.start(); // Start playing the FMOD event
    }

    public void GotNote()
    {
        pcSender.SendItem(noteItem);

        notePickupSound.start(); // Start playing the FMOD event
    }

    public void GotKey()
    {
        pcSender.SendItem(keyItem);
        keyGameObj.SetActive(true);
    }

    public void HammerPickupSound()
    {
        pickupSound.start();
    }
}


