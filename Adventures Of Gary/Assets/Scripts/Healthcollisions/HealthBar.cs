using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;
    public float cautionAmount = 0.65f;
    public float dangerAmount = 0.25f;

    private void Awake()
    {
        enabled = false;
    }
    public void UpdatePercentage(float percent)
    {
        float percentage = Mathf.Clamp(percent, 0.0f, 1.0f);

        healthBarFill.fillAmount = percentage;
        if(percentage <= dangerAmount)
        {
            healthBarFill.color = Color.red;
        }
        else if (percentage <= cautionAmount)
        {
            healthBarFill.color =Color.yellow;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
