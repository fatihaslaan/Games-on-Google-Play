using System;
using UnityEngine;

[Serializable]
public class AlienArmBehaviour : IArm
{
    public string Name()
    {
        return "Arm";
    }

    public float AttackSpeed()
    {
        return 1.25f;
    }

    public bool HasProjectile()
    {
        return false;
    }

    public float Cost()
    {
        return 1f;
    }

    public float Damage()
    {
        return 0.5f;
    }

    public GameObject Object()
    {
        return ArmManager.Instance().alienArms[0];
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