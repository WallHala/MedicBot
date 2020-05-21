using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDaySstem : MonoBehaviour
{
    Light LightSys;
    [SerializeField] float cycleSpeed=0.01f;

    private void Start()
    {
        LightSys = GetComponent<Light>();
        StartCoroutine(DayNightCycle());
    }
    

    IEnumerator DayNightCycle()
    {
        for(float i = 1; i >0;)
        {
            i -= Time.deltaTime * cycleSpeed;
            LightSys.intensity = i;
            yield return null;
        }

        for (float i=0; i <= 1;)
        {
            i += Time.deltaTime * cycleSpeed;
            LightSys.intensity = i;
            yield return null ;
        }

    }
}
