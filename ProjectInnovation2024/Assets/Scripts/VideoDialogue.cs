// VideoDialogue.cs
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public float delayBetweenLines = 1.0f;
    private int index;

    // Reference to the VideoPlayer component
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(AutoTypeLine());
    }

    IEnumerator AutoTypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // Wait for a delay between lines
        yield return new WaitForSeconds(delayBetweenLines);

        // Proceed to the next line
        NextLine();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(AutoTypeLine());
        }
        else
        {
            // The dialogue is finished
            gameObject.SetActive(false);
            // Optionally, you can add logic to load the next scene or perform other actions
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // You can keep the input check here if you want to manually proceed with the dialogue
        // For automatic playback, you may omit the input check
    }
}
