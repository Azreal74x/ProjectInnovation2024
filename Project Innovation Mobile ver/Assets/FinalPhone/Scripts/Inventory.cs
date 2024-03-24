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

  private Dictionary<string, GameObject> itemsMap = new Dictionary<string, GameObject>();
  private Dictionary<string, bool> itemsUsed = new Dictionary<string, bool>();
  private HashSet<string> itemsUnlocked = new HashSet<string>(); // tracking unlocked items

  public string currentItem = null; // name of the currently selected item

  private void Start() {
    InitializeItemsMap();
    // no need to initialize itemsUnlocked here since it's already being handled in InitializeItemsMap
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
        // Initially, items are not unlocked, so no action needed to add them to itemsUnlocked here
        correspondingButton.SetActive(false); // Initially, buttons are not visible
      }
    }
  }

  // Method to unlock items and make their buttons visible
  public void UnlockItem(string itemName) {
    if (itemsMap.ContainsKey(itemName) && !itemsUnlocked.Contains(itemName)) {
      itemsUnlocked.Add(itemName);
      var button = itemsButtons.transform.Find(itemName)?.gameObject;
      if (button != null) {
        button.SetActive(true); // Make the button visible
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

    // Update the currently selected item
    currentItem = buttonName;

    foreach (var item in itemsMap) {
      item.Value.SetActive(false); // Hide all sprites
    }

    if (itemsMap.ContainsKey(buttonName)) {
      itemsMap[buttonName].SetActive(true);
      ShowOrHideButtons(false); // Hide buttons except for the used ones
    }
  }

  public void OnBackButtonClick() {
    ShowOrHideButtons(true); // Show all buttons except for the used ones
    foreach (var sprite in itemsMap.Values) {
      sprite.SetActive(false); // Hide all sprites
    }
    showBackButton = false;
    DeselectCurrentItem();
    note.SetActive(true);
  }

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

  private void ShowOrHideButtons(bool show) {
    foreach (var item in itemsMap.Keys) {
      var button = itemsButtons.transform.Find(item)?.gameObject;
      if (button != null && itemsUnlocked.Contains(item)) {
        // Show or hide the button based on the 'show' parameter and whether the item is used
        button.SetActive(show && !itemsUsed[item]);
      }
    }
    // This line ensures that the parent container's active state reflects whether any buttons are to be shown
    itemsButtons.SetActive(show);
    showBackButton = !show;
    note.SetActive(show);
  }

  private void DeselectCurrentItem() {
    currentItem = null;
  }
}