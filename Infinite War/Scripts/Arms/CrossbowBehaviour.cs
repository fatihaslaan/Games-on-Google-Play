using System;
using UnityEngine;

[Serializable]
public class CrossbowBehaviour : IArm
{
    public string Name()
    {
        return "Crossbow";
    }

    public float AttackSpeed()
    {
        return 1.5f;
    }

    public bool HasProjectile()
    {
        return true;
    }

    public float Cost()
    {
        return 4f;
    }

    public float Damage()
    {
        return 0.65f;
    }

    public GameObject Object()
    {
        return ArmManager.Instance().humanArms[3];
    }

    public float Range()
    {
        return 12f;
    }

    public float Shield()
    {
        return 0f;
    }

    public float Weight()
    {
        return 0.3f;
    }

    public GlobalAttributes.Target Target()
    {
        return GlobalAttributes.Target.Warrior;
    }

    public GlobalAttributes.Type Type()
    {
        return GlobalAttributes.Type.Weapon;
    }
}