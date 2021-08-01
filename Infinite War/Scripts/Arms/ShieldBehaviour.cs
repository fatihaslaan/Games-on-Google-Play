using System;
using UnityEngine;

[Serializable]
public class ShieldBehaviour : IArm
{
    public string Name()
    {
        return "Shield";
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
        return 3f;
    }

    public float Damage()
    {
        return 0f;
    }

    public GameObject Object()
    {
        return ArmManager.Instance().humanArms[1];
    }

    public float Range()
    {
        return 2f;
    }

    public float Shield()
    {
        return 8f;
    }

    public float Weight()
    {
        return 0.5f;
    }

    public GlobalAttributes.Target Target()
    {
        return GlobalAttributes.Target.Warrior;
    }

    public GlobalAttributes.Type Type()
    {
        return GlobalAttributes.Type.Shield;
    }
}