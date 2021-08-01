using System;
using UnityEngine;

[Serializable]
public class AlienBombArmBehaviour : IArm
{
    public string Name()
    {
        return "Bomb";
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
        return 4f;
    }

    public GameObject Object()
    {
        return ArmManager.Instance().alienArms[3];
    }

    public float Range()
    {
        return 3.2f;
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
        return GlobalAttributes.Type.Explosive;
    }
}