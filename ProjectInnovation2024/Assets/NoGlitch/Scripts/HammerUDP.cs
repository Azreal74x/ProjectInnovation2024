using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using FMODUnity;

public class HammerUDP : MonoBehaviour {

  [SerializeField] private string hammerItem = "Hammer"; //has to be name of the item in the inventory
  [SerializeField] private string sulferItem = "Sulfer"; //has to be name of the item in the inventory

  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component
  private PcSender pcSender; //cache component

  private int swingCount = 0;
  private bool check = false;

  [SerializeField] private GameObject hammerObj;
  [SerializeField] private GameObject textBox;
  public bool hammer = false;

  [SerializeField] private GameObject potionObj;
  //public bool potion = false;

  //[SerializeField] Sprite brokenGlass;
  [SerializeField] private GameObject brokenGlass;
  //[SerializeField] private GameObject otherGlass;

  [SerializeField] private GameObject sulfurHitbox;

  private FMOD.Studio.EventInstance hammerSound;
  [FMODUnity.EventRef][SerializeField] private string fmodHammerSound;
  
  void Start() {
    if (senderListener != null) {
      pcListener = senderListener.GetComponent<PcListener>();
      pcSender = senderListener.GetComponent<PcSender>();
    }
    sulfurHitbox.SetActive(false);

    hammerSound = FMODUnity.RuntimeManager.CreateInstance(fmodHammerSound);
  }

  void Update() {

    if (pcListener.currentItem == "Hammer" && hammer && pcListener.accelerationSqrMagnitude > 30f) {
      swingCount += 1;
      hammerSound.start();
    }

    if (!check && swingCount >= 7) {
      //this.gameObject.GetComponent<SpriteRenderer>().sprite = brokenGlass;

      brokenGlass.SetActive(true);

      sulfurHitbox.SetActive(true);

      //otherGlass.SetActive(false);
      check = true;

      pcSender.SendUsedItem(hammerItem);
    }
  }

  public void PickedHammer() {
    hammer = true;
    hammerObj.SetActive(false);
    pcSender.SendItem(hammerItem);
  }

  public void PickedPotion() {
    if (check) {
      //potion = true;
      potionObj.SetActive(false);
      pcSender.SendItem(sulferItem);
    }
  }

}
