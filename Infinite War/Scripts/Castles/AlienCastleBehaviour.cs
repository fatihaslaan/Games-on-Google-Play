using UnityEngine;

public class AlienCastleBehaviour : CastleController
{
    public override void SpawnWarrior()
    {
        Instantiate(Warrior(), GlobalAttributes.GetSpawnPoint(collider, Warrior().transform.localScale.y * 1.5f), transform.rotation).AddComponent<AlienController>();
    }

    public override int Id()
    {
        return 1;
    }

    public override Vector3 Location()
    {
        return GlobalAttributes.GetSpawnPoint(collider, BattleSceneManager.Instance().human.transform.localScale.y * 1.5f);;
    }

    public override GameObject Warrior()
    {
        return BattleSceneManager.Instance().alien;
    }

    public override float Health()
    {
        return 50f;
    }

    public override int EnemyId()
    {
        return 0;
    }

    public override float SkillDamage()
    {
        return 18f;
    }

    public override float ProjectileDamage()
    {
        return 3f;
    }
}