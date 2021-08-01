using System;
using UnityEngine;

[Serializable]
public class AlienSharpArmBehaviour : IArm
{
    public string Name()
    {
        return "Sharp Arm";
    }

    public float AttackSpeed()
    {
        return 1f;
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
        return 1f;
    }

    public GameObject Object()
    {
        return ArmManager.Instance().alienArms[1];
    }

    public float Range()
    {
        return 3f;
    }

    public float Shield()
    {
        return 0f;
    }

    public float Weight()
    {
        return 0.4f;
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