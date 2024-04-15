using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ComicScript : MonoBehaviour
{

    private FMOD.Studio.EventInstance endMusic;
    [FMODUnity.EventRef][SerializeField] private string fmodEndMusic;

    // Start is called before the first frame update
    void Start()
    {
        endMusic = FMODUnity.RuntimeManager.CreateInstance(fmodEndMusic);
        endMusic.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Goodbye");
    }
}
