using System;
using UnityEngine;

[Serializable]
public class DynamiteBehaviour : IArm
{
    public string Name()
    {
        return "Dynamite";
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
        return 8f;
    }

    public float Damage()
    {
        return 8f;
    }

    public GameObject Object()
    {
        return ArmManager.Instance().humanArms[2];
    }

    public float Range()
    {
        return 2f;
    }

    public float Shield()
    {
        return 0f;
    }

    public float Weight()
    {
        return 0.1f;
    }

    public GlobalAttributes.Target Target()
    {
        return GlobalAttributes.Target.Castle;
    }

    public GlobalAttributes.Type Type()
    {
        return GlobalAttributes.Type.Explosive;
    }
}