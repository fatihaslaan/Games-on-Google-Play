using UnityEngine;

public interface IArm
{
    string Name();
    
    float Cost();

    bool HasProjectile(); //Has projectiles to shoot enemy

    GlobalAttributes.Type Type();

    GameObject Object();

    float Weight(); //Slows down to warrior

    float Damage();

    float AttackSpeed();

    float Shield();

    float Range();

    GlobalAttributes.Target Target(); //Target of weapon
}