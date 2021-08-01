using System;
using UnityEngine;

[Serializable]
public class EmptyArmBehaviour : IArm
{
    public string Name()
    {
        return "None";
    }

    public float AttackSpeed()
    {
        return 0f;
    }

    public bool HasProjectile()
    {
        return false;
    }

    public float Cost()
    {
        return 0f;
    }

    public float Damage()
    {
        return 0f;
    }

    public GameObject Object()
    {
        return null;
    }

    public float Range()
    {
        return 0f;
    }

    public float Shield()
    {
        return 0f;
    }

    public float Weight()
    {
        return 0f;
    }

    public GlobalAttributes.Target Target()
    {
        return GlobalAttributes.Target.Castle;
    }

    public GlobalAttributes.Type Type()
    {
        return GlobalAttributes.Type.None;
    }
}