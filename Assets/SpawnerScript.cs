using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ProtonSpawner : MonoBehaviour
{
    public GameObject Proton;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;  // Ensure correct placement at 2D plane
            Instantiate(Proton, mousePosition, Quaternion.identity);
        }
    }

}