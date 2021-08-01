using System;
using UnityEngine;

[Serializable]
public class AlienShieldArmBehaviour : IArm
{
    public string Name()
    {
        return "Shield Arm";
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
        return 2f;
    }

    public float Damage()
    {
        return 0f;
    }

    public GameObject Object()
    {
        return ArmManager.Instance().alienArms[2];
    }

    public float Range()
    {
        return 2f;
    }

    public float Shield()
    {
        return 3f;
    }

    public float Weight()
    {
        return 0.35f;
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