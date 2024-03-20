using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    { }

    public void clickAndBegone()
    {
        gameObject.SetActive(false);
    }
}
