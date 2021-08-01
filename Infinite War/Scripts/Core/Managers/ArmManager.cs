using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour
{
    public List<GameObject> humanArms; //All gun objects are stored in this singleton
    public List<GameObject> alienArms;

    static ArmManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public static ArmManager Instance()
    {
        return instance;
    }
}