using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helthplayer : MonoBehaviour
{
    public float maxHelath = 100;
    public float currentHealth;
    public helf helthbar;
    void Start()
    {
        currentHealth = maxHelath;

        helthbar.setmaxhealth(maxHelath);
    }

  
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag=="con")
        {
            Takedemage(10);
        }
    }

    void Takedemage(float demage)
    {
        currentHealth -= demage*Time.deltaTime;
        helthbar.sethelth(currentHealth);
    }

    
}
