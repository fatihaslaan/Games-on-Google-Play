using System;
using UnityEngine;

[Serializable]
public class AlienDartArmBehaviour : IArm
{
    public string Name()
    {
        return "Dart";
    }

    public float AttackSpeed()
    {
        return 2f;
    }

    public bool HasProjectile()
    {
        return true;
    }

    public float Cost()
    {
        return 2f;
    }

    public float Damage()
    {
        return 0.05f;
    }

    public GameObject Object()
    {
        return ArmManager.Instance().alienArms[4];
    }

    public float Range()
    {
        return 14f;
    }

    public float Shield()
    {
        return 0f;
    }

    public float Weight()
    {
        return 0.2f;
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