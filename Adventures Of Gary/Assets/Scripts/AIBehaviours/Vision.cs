using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    private AIBrain brain;

    private void Awake()
    {
        brain = GetComponentInParent<AIBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            brain.SpotEnemy(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            brain.LostEnemy();
        }
    }
}
