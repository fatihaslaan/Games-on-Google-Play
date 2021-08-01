using System.Collections.Generic;

public class AlienBehaviour : WarriorBehaviour
{
    public string Name()
    {
        return "Alien";
    }

    public List<IArm> Arms()
    {
        return GlobalAttributes.alienArms;
    }

    public CastleController EnemyCastle()
    {
        return BattleSceneManager.Instance().humanCastle.GetComponent<CastleController>();
    }
    public float Health()
    {
        return 15f;
    }

    public float SightDistance()
    {
        return 10f;
    }

    public float SpeedMultiplier()
    {
        return 6f;
    }

    public int NumberOfArms()
    {
        return 4;
    }

    public float AttackSpeedMultiplier()
    {
        return 1.5f;
    }

    public float DamageMultiplier()
    {
        return 1.5f;
    }

    public bool ExplosionOnDeath()
    {
        return false;
    }
}