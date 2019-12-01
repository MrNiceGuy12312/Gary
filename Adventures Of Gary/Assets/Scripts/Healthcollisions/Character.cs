using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHitPoints = 100;
    private int curHitPoints;
    private HealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }
    void Start()
    {
        curHitPoints = maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void ApplyDamage(int amount)
    {
        curHitPoints -= amount;
        if (healthBar != null)
        {
            healthBar.UpdatePercentage((float)curHitPoints / maxHitPoints);
        }
    }
}
