using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    [SerializeField] private string firstScene = "RoomUV";
    [SerializeField] private string lastScene = "RoomPourPotion";


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GoLeftScene()
    {
        if(SceneManager.GetActiveScene().name == firstScene)
        {
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void GoRightScene()
    {
        if(SceneManager.GetActiveScene().name == lastScene)
        {
            SceneManager.LoadScene(firstScene);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
}
