using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Image imageComponent; // Reference to the Image component for displaying images
    public string[] lines;
    public Sprite[] images; // Array of images corresponding to each voice line
    public float textSpeed;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        imageComponent.sprite = null; // Set the initial image to null
    }

    // Call this method to trigger the in-game dialogue
    public void TriggerDialogue()
    {
        gameObject.SetActive(true);
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // You can keep the input check here if you want to manually proceed with the dialogue
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                // Display the corresponding image
                if (index < images.Length)
                {
                    imageComponent.sprite = images[index];
                }
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // Display the corresponding image after typing the text
        if (index < images.Length)
        {
            imageComponent.sprite = images[index];
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            imageComponent.sprite = null; // Clear the image when moving to the next line
            StartCoroutine(TypeLine());
        }
        else
        {
            // The dialogue is finished
            gameObject.SetActive(false);
            // Optionally, you can add logic to continue with the in-game scenario or perform other actions
        }
    }
}
