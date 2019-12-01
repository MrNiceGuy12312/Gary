﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemydamage : MonoBehaviour
{
    public int minDamage = 1;
    public int maxDamage = 5;

    private void OnTriggerEnter(Collider other)
    {
        Character script = other.GetComponent<Character>();
        if (script != null)
        {
            script.ApplyDamage(Random.Range(minDamage, maxDamage));
        }
    }
}
