using System;
using UnityEngine;

[Serializable]
public class WandBehaviour : IArm
{
    public string Name()
    {
        return "Wand";
    }

    public float AttackSpeed()
    {
        return 1f;
    }

    public bool HasProjectile()
    {
        return true;
    }

    public float Cost()
    {
        return 6f;
    }

    public float Damage()
    {
        return 1.2f;
    }

    public GameObject Object()
    {
        return ArmManager.Instance().humanArms[4];
    }

    public float Range()
    {
        return 10f;
    }

    public float Shield()
    {
        return 0f;
    }

    public float Weight()
    {
        return 0.25f;
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