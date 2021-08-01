using System.Collections.Generic;

public class HumanBehaviour : WarriorBehaviour
{
    public string Name()
    {
        return "Human";
    }

    public List<IArm> Arms()
    {
        return GlobalAttributes.humanArms;
    }

    public CastleController EnemyCastle()
    {
        return BattleSceneManager.Instance().alienCastle.GetComponent<CastleController>();
    }

    public float Health()
    {
        return 10f;
    }

    public float SightDistance()
    {
        return 6f;
    }

    public float SpeedMultiplier()
    {
        return 4f;
    }

    public int NumberOfArms()
    {
        return 2;
    }

    public float AttackSpeedMultiplier()
    {
        return 1f;
    }

    public float DamageMultiplier()
    {
        return 1f;
    }

    public bool ExplosionOnDeath()
    {
        return true;
    }
}