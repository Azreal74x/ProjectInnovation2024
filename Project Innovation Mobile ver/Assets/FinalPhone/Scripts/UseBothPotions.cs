using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBothPotions : MonoBehaviour {
  [SerializeField] private Inventory inventory;
  private string itemName = "PotionPurple";
  private string otherItemName = "PotionRed";
  private bool used = false;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if (used) {
      inventory.MarkItemAsUsed(itemName);
      inventory.MarkItemAsUsed(otherItemName);
    }
  }

  public void Collect() {
    used = true;
  }
}
