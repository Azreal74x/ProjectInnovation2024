using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSwitching : MonoBehaviour {

  [SerializeField] private List<GameObject> screens = new List<GameObject>(); //setup array list
  [SerializeField] private List<GameObject> spritesToFade = new List<GameObject>(); // GameObjects with SpriteRenderer components

  [SerializeField] private GameObject leftArrowButton;
  [SerializeField] private GameObject rightArrowButton;
  [SerializeField] private GameObject downArrowButton;
  [SerializeField] private GameObject breweryButton;
  [SerializeField] private GameObject officeButton;

  [SerializeField] private GameObject brewery;
  [SerializeField] private GameObject office;

  public int currentScreen = 0;
  [SerializeField] private Image blackBackground;
  [SerializeField] private float fadeDuration = 0.3f;

  void Start() {
    blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 0f);
  }

  void Update() {

  }

  private IEnumerator FadeToBlackAndSwitch(int nextScreen) {
    StartCoroutine(FadeSpritesToColorAndBack(Color.black, fadeDuration + fadeDuration));

    yield return StartCoroutine(FadeToBlack()); // Fade to black
    ActivateScreen(nextScreen); // Activate the next screen
    yield return StartCoroutine(FadeFromBlack()); // Fade back in

  }

  private IEnumerator FadeToBlack() {
    float currentTime = 0f;
    while (currentTime <= fadeDuration) {
      float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);
      blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, alpha);
      currentTime += Time.deltaTime;
      yield return null;
    }
  }

  private IEnumerator FadeFromBlack() {
    float currentTime = 0f;
    while (currentTime <= fadeDuration) {
      float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
      blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, alpha);
      currentTime += Time.deltaTime;
      yield return null;
    }
  }

  private IEnumerator FadeSpritesToColorAndBack(Color targetColor, float duration) {
    // Ensure the operation starts only if there are sprites to fade.
    if (spritesToFade.Count == 0) yield break;

    float halfDuration = duration / 2f; // Split the duration for fading to color and then back to original
    List<Color> originalColors = new List<Color>(); // To keep track of the original colors.

    // Store the original color of each sprite.
    foreach (var sprite in spritesToFade) {
      SpriteRenderer renderer = sprite.GetComponent<SpriteRenderer>();
      if (renderer != null) {
        originalColors.Add(renderer.color);
      }
    }

    // First half: Fade to target color
    float currentTime = 0f;
    while (currentTime <= halfDuration) {
      currentTime += Time.deltaTime;
      float lerpFactor = currentTime / halfDuration;
      for (int i = 0; i < spritesToFade.Count; i++) {
        if (spritesToFade[i].GetComponent<SpriteRenderer>() != null) {
          spritesToFade[i].GetComponent<SpriteRenderer>().color = Color.Lerp(originalColors[i], targetColor, lerpFactor);
        }
      }
      yield return null;
    }

    // Second half: Fade back to original colors
    currentTime = 0f; // Reset currentTime for the fade back.
    while (currentTime <= halfDuration) {
      currentTime += Time.deltaTime;
      float lerpFactor = currentTime / halfDuration;
      for (int i = 0; i < spritesToFade.Count; i++) {
        if (spritesToFade[i].GetComponent<SpriteRenderer>() != null) {
          spritesToFade[i].GetComponent<SpriteRenderer>().color = Color.Lerp(targetColor, originalColors[i], lerpFactor);
        }
      }
      yield return null;
    }
  }

  private void ActivateScreen(int nextScreen) {
    screens[currentScreen].SetActive(false); // Deactivate the current screen
    currentScreen = nextScreen; // Update the current screen index
    CheckCurrentScreen(); // Ensure the new index is valid
    screens[currentScreen].SetActive(true); // Activate the new screen
    leftArrowButton.SetActive(true);
    rightArrowButton.SetActive(true);
    brewery.SetActive(false);
    downArrowButton.SetActive(false);
    office.SetActive(false);
  }

  private void CheckCurrentScreen() {
    if (currentScreen < 0) {
      currentScreen = screens.Count - 1;
    }
    else if (currentScreen >= 4) {
      currentScreen = 0;
    }

    if (currentScreen == 2) {
      breweryButton.SetActive(true);
    }
    if (currentScreen == 1) {
      office.SetActive(true);
    }
  }

  public void LeftArrowClick() {
    int nextScreen = (currentScreen - 1 + screens.Count) % screens.Count;
    StartCoroutine(FadeToBlackAndSwitch(nextScreen));

  }

  public void RightArrowClick() {
    int nextScreen = (currentScreen + 1) % screens.Count;
    StartCoroutine(FadeToBlackAndSwitch(nextScreen));

  }

  public void BackArrowClick() {
    StartCoroutine(FadeToBlackAndSwitch(currentScreen));

  }

  public void TurnOnBrewery() {
    foreach (var screen in screens) {
      screen.SetActive(false);
    }
    leftArrowButton.SetActive(false);
    rightArrowButton.SetActive(false);
    breweryButton.SetActive(false);

    downArrowButton.SetActive(true);
    brewery.SetActive(true);
  }

  public void TurnonSideroom() {
    foreach (var screen in screens) {
      screen.SetActive(false);
    }
    leftArrowButton.SetActive(false);
    rightArrowButton.SetActive(false);
    office.SetActive(false);

    downArrowButton.SetActive(true);
    office.SetActive(true);
  }
}
