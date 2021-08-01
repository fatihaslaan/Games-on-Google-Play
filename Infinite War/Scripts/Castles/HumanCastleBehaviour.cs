using UnityEngine;

public class HumanCastleBehaviour : CastleController
{
    public override void SpawnWarrior()
    {
        Instantiate(Warrior(), GlobalAttributes.GetSpawnPoint(collider, Warrior().transform.localScale.y * 1.5f), transform.rotation).AddComponent<HumanController>();
    }

    public override int Id()
    {
        return 0;
    }

    public override Vector3 Location()
    {
        return GlobalAttributes.GetSpawnPoint(collider, BattleSceneManager.Instance().alien.transform.localScale.y * 1.5f); ;
    }

    public override GameObject Warrior()
    {
        return BattleSceneManager.Instance().human;
    }

    public override float Health()
    {
        return 100f;
    }

    public override int EnemyId()
    {
        return 1;
    }

    public override float SkillDamage()
    {
        return 15f;
    }

    public override float ProjectileDamage()
    {
        return 1.5f;
    }
}