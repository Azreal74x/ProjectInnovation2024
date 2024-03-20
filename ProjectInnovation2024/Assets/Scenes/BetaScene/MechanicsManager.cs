using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class MechanicsManager : MonoBehaviour
{

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
    private bool potion = false;

    [SerializeField] private GameObject arrowLeft;
    [SerializeField] private GameObject arrowRight;

    [SerializeField] private GameObject potionToPour;


    [SerializeField] private GameObject roomBrewery;
    [SerializeField] private GameObject roomKey;


    private void Start()
    {

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
            pickups[0].SetActive(true);
        }
        else if (room != 0)
        {
            pickups[0].SetActive(false);
        }
        ////////////
        if (room == 1)
        {
            pickups[1].SetActive(true);
            /* if (pickups[3].GetComponent<MixUDP>().check) //activate throw
             {
                 pickups[4].SetActive(true);
             }
             if (pickups[4].GetComponentInChildren<ThrowUDP>().explosion) // after thrown activate door
             {
                 pickups[5].SetActive(true);
             }*/
        }
        else if (room != 1)
        {
            pickups[1].SetActive(false);
            /*pickups[4].SetActive(false);
            pickups[5].SetActive(false);*/
        }
        ////////////
        if (room == 2)  //  room 2  pour & mix potion
        {
            pickups[2].SetActive(true);
            /*
            pickups[3].SetActive(true);
            if (pickups[1].GetComponentInChildren<HammerUDP>().potion) // if we took the potion
            {
                potionToPour.SetActive(true); // set to true the vessel wuth mix in it
            }*/
        }
        else if (room != 2)
        {
            pickups[2].SetActive(false);
            //pickups[3].SetActive(false);
        }
        
        ////////////
        
        if (roomBrewery.activeInHierarchy)  //      zoom in room 2 
        {
            Debug.Log("active? " + roomBrewery.activeInHierarchy);
            pickups[3].SetActive(true);

            /*if (pickups[1].GetComponentInChildren<HammerUDP>().potion) // if we took the potion
            {
                potionToPour.SetActive(true); // set to true the vessel wuth mix in it
            }*/
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
            Debug.Log("active? " + roomBrewery.activeInHierarchy);
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


    public void OpenPour()
    {
        if (pickups[1].GetComponentInChildren<HammerUDP>().potion)
        {

            potionToPour.SetActive(!potionToPour.activeSelf);
        }
        else Debug.Log("no potion");
    }


}
