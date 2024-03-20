using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SquareUse : MonoBehaviour {

  [SerializeField] private Image useButton;
  public Color useColor = Color.green;
  public Color disabledColor = Color.gray;
  private bool canUse = false;

  
  void Start() {

  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.U)) {
      if (!canUse) {
        canUse = true;
      }
      else {
        canUse = false;
      }
    }

    if (canUse) {
      useButton.color = useColor;
      Use();
    }
    else {
      useButton.color = disabledColor;
    }
  }

  void Use() {
    Debug.Log("I can be used");
  }
}
