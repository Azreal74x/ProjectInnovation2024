using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowUDP : MonoBehaviour {
  [SerializeField] private string item = "PotionNew"; //has to be name of the item in the inventory

  [SerializeField] private GameObject senderListener;
  private PcListener pcListener; //cache component

  [SerializeField] private GameObject sideRoomButton;
  public bool explosion = false;

  [SerializeField] private GameObject ExplodedDoor;

  void Start() {
    if (senderListener != null) {
      pcListener = senderListener.GetComponent<PcListener>();
    }
  }

  void Update() {
    if (pcListener.accelerationSqrMagnitude > 30f) {
      //Explosion();
      SwitchDoor();
    }
  }

  private void Explosion() {
    sideRoomButton.SetActive(true);
    explosion = true;
  }

  private void SwitchDoor() {
    ExplodedDoor.SetActive(true);
  }
}
