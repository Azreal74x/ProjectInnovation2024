using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class MechanicsManager : MonoBehaviour
{
    [SerializeField] private GameObject senderListener;
    private PcSender pcSender; //cache component

    [SerializeField] private string waterItem = "Water"; //has to be name of the item in the inventory
    [SerializeField] private string uvLightItem = "UVLight"; //has to be name of the item in the inventory
    [SerializeField] private string noteItem = "Note"; //has to be name of the item in the inventory

    [SerializeField] private GameObject backgroundSwitchingController;
    private BackgroundSwitching backgroundSwitching;

    [SerializeField] private List<GameObject> pickups;
    //[SerializeField] private List<GameObject> backgrounds;

    //[SerializeField] private GameObject lore;

    //[SerializeField] private GameObject sulfur;
    //[SerializeField] private GameObject water;
    //[SerializeField] private GameObject hammer;

    //[SerializeField] private GameObject note;
    //[SerializeField] private GameObject uvLight;

    //[SerializeField] private GameObject key;


    private bool interactedWithUV = false;
    private bool sulfur = false;

    [SerializeField] private GameObject arrowLeft;
    [SerializeField] private GameObject arrowRight;

    [SerializeField] private GameObject potionToPour;


    [SerializeField] private GameObject roomBrewery;
    [SerializeField] private GameObject roomKey;




    private void Start()
    {
        if (senderListener != null)
        {
            pcSender = senderListener.GetComponent<PcSender>();
        }

        backgroundSwitching = backgroundSwitchingController.GetComponent<BackgroundSwitching>();
    }

    private void Update()
    {


        //Debug.Log(currentScreen);

        //Debug.Log("hammer? " + pickups[1].GetComponentInChildren<HammerUDP>().hammer);

        SwitchMechanics(backgroundSwitching.currentScreen);

    }
    private void SwitchMechanics(int room)
    {

        /*if (pickups[0].GetComponentInChildren<UVLightUDP>().discovered)
        {
            interactedWithUV = true;
        }



        else if (room != 0)
        {
            pickups[0].SetActive(false);
        }

        if (room != 0)
        {
            pickups[0].SetActive(false);
        }

        ////////////
        if (room == 1)  
        {
           */
        if (room == 0)
        {
            pickups[0].SetActive(true); // 
            /* if (pickups[3].GetComponent<MixUDP>().check) //activate throw
             {
                 pickups[4].SetActive(true);
             }
             if (pickups[4].GetComponentInChildren<ThrowUDP>().explosion) // after thrown activate door
             {
                 pickups[5].SetActive(true);
             }*/
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
            //pickups[3].SetActive(false);
        }
        
        ////////////
        
        if (roomBrewery.activeInHierarchy)  //      zoom in room 2 
        {
            //Debug.Log("active? " + roomBrewery.activeInHierarchy);
            pickups[3].SetActive(true);
            pickups[2].SetActive(false);

            if (sulfur) // if we took the potion
            {
                potionToPour.SetActive(true); // show the vessel wuth mix in it
            }
        }
        else if (!roomBrewery.activeSelf)
        {
            /*pickups[6].SetActive(false);

            pickups[2].SetActive(false);*/

            pickups[3].SetActive(false);
        }
        //////////////
        if (roomKey.activeInHierarchy)  //      zoom in room 2 
        {
            //Debug.Log("active? " + roomBrewery.activeInHierarchy);
            pickups[4].SetActive(true);
        }
        else if (!roomBrewery.activeSelf)
        {
            pickups[4].SetActive(false);


        }


        /* }
         else if (room != 4)
         {
             pickups[6].SetActive(false);

             pickups[2].SetActive(false);
             pickups[3].SetActive(false);
         }*/
    }


    public void InteractedWithUV()
    {
        interactedWithUV = true;
    }



    public void GotSulfur()
    {
        sulfur = true;
    }

    public void GotWater()
    {
        pcSender.SendItem(waterItem);
    }

    public void GotUVFlashlight()
    {
        pcSender.SendItem(uvLightItem);

    }

  public void GotNote() {
    pcSender.SendItem(noteItem);
  }


}
