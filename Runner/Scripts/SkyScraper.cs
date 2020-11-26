using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScraper : MonoBehaviour
{
    Light light;
    float time=0f;
    float maxtime=1.5f;
    float mintime=0.2f;
    float maxintensity;
    void Start()
    {
        light=this.GetComponentInChildren<Light>();
        maxintensity=light.intensity;
        time=Random.Range(mintime,maxtime);
    }

    void Update()
    {
        if(time>0)
        {
            time-=Time.deltaTime; 
            if(light.enabled&&(int)time%2==0)
            {
                light.intensity=Random.Range(10,maxintensity);
            }
        }
        if(time<=0)
        {
            light.enabled=!light.enabled;
            time=Random.Range(mintime,maxtime);
        }
    }
}
