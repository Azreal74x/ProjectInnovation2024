using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

  [SerializeField] private GameObject itemsButtons;
  [SerializeField] private GameObject itemsSprites;
  [SerializeField] private GameObject backButton;
  [SerializeField] private GameObject note;

  private bool showBackButton = false;

  [SerializeField] private PhoneListener phoneListener;

  private Dictionary<string, GameObject> itemsMap = new Dictionary<string, GameObject>();
  private Dictionary<string, bool> itemsUsed = new Dictionary<string, bool>();

  private void Start() {
    InitializeItemsMap();
    phoneListener = GetComponent<PhoneListener>();
    //phoneSender.setIP
  }

  private void Update() {
    CheckBackButton();
  }
  
  void CheckBackButton() {
    if (showBackButton) {
      backButton.SetActive(true);
    }
    else {
      backButton?.SetActive(false);
    }
  }

  void InitializeItemsMap() {
    foreach (Transform sprite in itemsSprites.transform) {
      GameObject correspondingButton = itemsButtons.transform.Find(sprite.name)?.gameObject;

      if (correspondingButton != null) {
        itemsMap[correspondingButton.name] = sprite.gameObject;
        itemsUsed[correspondingButton.name] = false;
      }
    }
  }

  public void ButtonClick() {
    string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
    OnButtonClick(buttonName);
  }

  private void OnButtonClick(string buttonName) {
    if (itemsUsed.ContainsKey(buttonName) && itemsUsed[buttonName]) {
      // If the item is used, do nothing or handle accordingly
      return;
    }

    foreach (var item in itemsMap) {
      item.Value.SetActive(false); // Hide all sprites
    }

    if (itemsMap.ContainsKey(buttonName)) {
      itemsMap[buttonName].SetActive(true);
      ShowOrHideButtons(false); // Hide buttons except used ones
    }
  }

  public void OnBackButtonClick() {
    ShowOrHideButtons(true); // Show all buttons except used ones
    foreach (var sprite in itemsMap.Values) {
      sprite.SetActive(false); // Hide all sprites
    }
    showBackButton = false;
    note.SetActive(true);
  }

  // Method to mark an item as used, callable from other scripts
  public void MarkItemAsUsed(string itemName) {
    if (itemsUsed.ContainsKey(itemName)) {
      itemsUsed[itemName] = true;

      // Disable the button for the used item
      var button = itemsButtons.transform.Find(itemName)?.gameObject;
      if (button != null) {
        button.SetActive(false);
      }

      // If the sprite of the used item is currently active, hide it
      if (itemsMap.ContainsKey(itemName) && itemsMap[itemName].activeSelf) {
        itemsMap[itemName].SetActive(false);

        // Automatically "go back" to showing the correct set of buttons
        ShowOrHideButtons(true);
      }
    }
  }

  // Ensure to update ShowOrHideButtons method if it's not correctly managing the parent object's active state
  private void ShowOrHideButtons(bool show) {
    foreach (var item in itemsUsed) {
      var button = itemsButtons.transform.Find(item.Key)?.gameObject;
      if (button != null) {
        // Show or hide the button based on the 'show' parameter and whether the item is used
        button.SetActive(show && !item.Value);
      }
    }
    // This line ensures that the parent container's active state reflects whether any buttons are to be shown
    itemsButtons.SetActive(show);
    showBackButton = true;
    note.SetActive(false);
  }
}
