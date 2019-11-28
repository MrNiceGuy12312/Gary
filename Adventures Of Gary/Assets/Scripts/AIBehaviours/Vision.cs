using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Vision : MonoBehaviour
{
    public AudioSource fight;
    private AIBrain brain;

    private void Start()
    {
        fight = GetComponent<AudioSource>();
        
    }
    private void Awake()
    {
        brain = GetComponentInParent<AIBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fight.Play();
            brain.SpotEnemy(other.transform);
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fight.Stop();
            brain.LostEnemy();
            
        }
    }
}
