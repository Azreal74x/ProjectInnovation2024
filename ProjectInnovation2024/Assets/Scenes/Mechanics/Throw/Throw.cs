using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{

    [SerializeField] private GameObject explosionImage;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.acceleration.sqrMagnitude > 30f)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        explosionImage.SetActive(true);
    }

}
