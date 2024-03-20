using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UseExplosion : MonoBehaviour {

  [SerializeField] private Inventory inventory;
  private string itemName = "Explosion";
  private bool used = false;


  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if (used) {
      inventory.MarkItemAsUsed(itemName);
    }
  }

  public void Collect() {
    used = true;
  }

}
