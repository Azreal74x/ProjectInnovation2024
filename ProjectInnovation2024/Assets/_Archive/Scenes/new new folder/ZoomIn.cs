using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    [SerializeField] private GameObject arrowLeft;
    [SerializeField] private GameObject arrowRight;

    [SerializeField] private GameObject arrowDown;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            arrowLeft.SetActive(false);
            arrowRight.SetActive(false);

            arrowDown.SetActive(true);
        }
        
    }

    public void HideArrows()
    {

        arrowLeft.SetActive(false);
        arrowRight.SetActive(false);

        arrowDown.SetActive(true);

        this.gameObject.SetActive(true);
    }

    public void ArrowDown()
    {
        arrowLeft.SetActive(true);
        arrowRight.SetActive(true);

        arrowDown.SetActive(false);

        this.gameObject.SetActive(false);
    }



}
