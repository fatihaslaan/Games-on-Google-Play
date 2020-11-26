using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Infinity : MonoBehaviour
{
    public GameObject SkyScraper;
    public GameObject Platform;
    public GameObject Bounds;
    List<GameObject> loadedplatforms = new List<GameObject>();
    List<GameObject> loadedbounds = new List<GameObject>();
    float topfarplatform = 0;
    float topfarbounds = 0;
    float pdistance = 0f;
    void Start()
    {
        loadbounds();
        if (Global.modeindex == 0)
        {
            pdistance = 200f;
            Platform.GetComponentInChildren<Light>().color = Global.colors[Global.colorindex];
            loadplatforms(25, 265, 0, 450, Platform);
        }
        else
        {
            pdistance = 250f;
            SkyScraper.GetComponentInChildren<Light>().color = Global.colors[Global.colorindex];
            loadplatforms(15, 265, -620, -200, SkyScraper);
        }
        topfarbounds = Bounds.transform.position.z - 560;

    }
    void loadbounds()
    {
        for (int i = 0; i < 10; i++)
        {
            loadedbounds.Add(Instantiate(Bounds, new Vector3(Bounds.transform.position.x, Bounds.transform.position.y, topfarbounds + 560), Quaternion.identity));
            topfarbounds += 560;
        }
    }
    void loadplatforms(int quantity, int xval, int ymin, int ymax, GameObject obj)
    {
        float platformtemp = 0;
        for (int i = 0; i < quantity; i++)
        {
            float x, y, z;
            int index = 0;
            do
            {
                index++;
                x = Random.Range(-xval, xval);
                y = Random.Range(ymin, ymax);
                z = Random.Range(200 + topfarplatform, 1100 + topfarplatform);
                if (z < 500 && Global.modeindex == 1)
                {
                    y = Random.Range(-620, -380);
                }
                if (index > 1000)
                {
                    Debug.Log("Error:Infinity");
                    break;
                }

            } while (!isfar(new Vector3(x, y, z)));
            if (index < 1000)
            {
                if (z > platformtemp)
                    platformtemp = z;
                loadedplatforms.Add(Instantiate(obj, new Vector3(x, y, z), Quaternion.identity));
            }
        }
        topfarplatform = platformtemp;
    }

    bool isfar(Vector3 v)
    {
        foreach (GameObject o in loadedplatforms)
        {
            if (Global.modeindex == 0){
                if (Vector3.Distance(v, o.transform.position) < pdistance)
                {
                    return false;
                }
            }
            else{
                if (Vector2.Distance(new Vector2(v.x, v.z), new Vector2(o.transform.position.x, o.transform.position.z)) < pdistance)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void RemovePlatforms()
    {
        for (int i = 0; i < loadedplatforms.Count; i++)
        {
            if (loadedplatforms[i].transform.position.z < Global.playerpos - 50)
            {
                Destroy(loadedplatforms[i]);
                loadedplatforms.RemoveAt(i);
                Debug.Log("Removed");
            }
        }
    }
    void Update()
    {
        if (Global.landed)
        {
            foreach (GameObject p in loadedplatforms)
            {
                if (p.transform.position.z < Global.playerpos+100 || p.transform.position.z > Global.playerpos + 1000)
                    p.GetComponentInChildren<Light>().enabled = false;
                else
                    p.GetComponentInChildren<Light>().enabled = true;
            }
            if (Global.playerpos > topfarplatform - 800)
            {
                RemovePlatforms();
                loadbounds();
                if(Global.modeindex==0)
                    loadplatforms(25, 265, 0, 450, Platform);
                else
                    loadplatforms(15, 265, -620, -200, SkyScraper);
                Debug.Log("Objects Refreshed");
            }
            Global.landed = false;
        }
    }
}
