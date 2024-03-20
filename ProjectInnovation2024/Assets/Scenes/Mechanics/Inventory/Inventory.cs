using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

  [SerializeField] private List<GameObject> items = new List<GameObject>(); //setup array list
  private int currentItem = 0; //bool to keep track of the current item

  private Vector2 startTouchPosition;
  private Vector2 endTouchPosition;

  private void Start() {

  }

  private void Update() {
    ActivateItem();
    CheckCurrentItem();
    SwipeTouch();
  }

  private void ActivateItem() { //takes control of activating items
    for(int i = 0; i < items.Count; i++) { 
      if (i == currentItem) {
        items[i].SetActive(true); 
      }
      else {
        items[i].SetActive(false);
      }
    }
  }

  private void CheckCurrentItem() { //make sure current item doesn't exceed the list
    if (currentItem < 0) {
      currentItem = items.Count - 1;
    }
    if (currentItem > items.Count - 1) {
      currentItem = 0;
    }
  }

  private void SwipeTouch() {
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
      startTouchPosition = Input.GetTouch(0).position;
    }

    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
      endTouchPosition = Input.GetTouch(0).position;

      if (endTouchPosition.x < startTouchPosition.x) {
        currentItem += 1;
      }

      if (endTouchPosition.x > startTouchPosition.x) {
        currentItem -= 1;
      }
    }
  }

  public void LeftArrowClick() { //left button click
     currentItem -= 1;
  }

  public void RightArrowClick() { //right button click
     currentItem += 1;
  }

}
