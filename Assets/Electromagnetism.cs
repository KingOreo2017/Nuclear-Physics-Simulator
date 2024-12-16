using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electromagnetism : MonoBehaviour
{
    private GameObject[] cachedProtons;
    private Rigidbody[] cachedProtonBodies;
    private GameObject[] cachedElectrons;
    private Rigidbody[] cachedElectronBodies;
    float particleMass = 1836.23f;
    float forceDecayFactor = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Cache Protons

        cachedProtons = GameObject.FindGameObjectsWithTag("Proton");
        cachedProtonBodies = new Rigidbody[cachedProtons.Length];

        for (int i = 0; i < cachedProtons.Length; i++)
        {
            cachedProtonBodies[i] = cachedProtons[i].GetComponent<Rigidbody>();
        }

        // Cache Eletrons

        cachedElectrons = GameObject.FindGameObjectsWithTag("Electron");
        cachedElectronBodies = new Rigidbody[cachedElectrons.Length];

        for (int i = 0; i < cachedElectrons.Length; i++)
        {
            cachedElectronBodies[i] = cachedElectrons[i].GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Electromagnetic Force

        // Push Protons away from each other
        for (int i = 0; i < cachedProtons.Length; i++)
        {
            for (int j = i + 1; j < cachedProtons.Length; j++)
            {
                Vector3 directionOfForce = cachedProtonBodies[i].position - cachedProtonBodies[j].position;
                float distance = directionOfForce.magnitude;
                directionOfForce = directionOfForce.normalized;
                float forceMagnitude = forceDecayFactor / (distance * distance);
                cachedProtonBodies[i].AddForce(directionOfForce * forceMagnitude, ForceMode.Force);
                cachedProtonBodies[j].AddForce(-directionOfForce * forceMagnitude, ForceMode.Force);
            }
        }

        // Push Eletrons away from each other
        for (int i = 0; i < cachedElectrons.Length; i++)
        {
            for (int j = i + 1; j < cachedElectrons.Length; j++)
            {
                Vector3 directionOfForce = cachedElectronBodies[i].position - cachedElectronBodies[j].position;
                float distance = directionOfForce.magnitude;
                directionOfForce = directionOfForce.normalized;
                float forceMagnitude = forceDecayFactor / (distance * distance);
                cachedElectronBodies[i].AddForce(directionOfForce * forceMagnitude, ForceMode.Force);
                cachedElectronBodies[j].AddForce(-directionOfForce * forceMagnitude, ForceMode.Force);
            }
        }

        // Pull Protons and Electrons together
        for (int i = 0; i < cachedElectrons.Length; i++)
        {
            for (int j = i; j < cachedProtons.Length; j++)
            {
                Vector3 directionOfForce = cachedElectronBodies[i].position - cachedProtonBodies[j].position;
                float distance = directionOfForce.magnitude;
                directionOfForce = directionOfForce.normalized;
                float forceMagnitude = -(distance * distance) + forceDecayFactor;
                cachedElectronBodies[i].AddForce(-directionOfForce * forceMagnitude, ForceMode.Force);
                cachedProtonBodies[j].AddForce(directionOfForce * forceMagnitude, ForceMode.Force);
            }
        }


    }
}
