using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class WarriorController : BaseController
{
    NavMeshAgent agent;

    List<Transform> armPath = new List<Transform>();
    Animation[] animations;

    BaseController target;
    Vector3 tempTarget;
    WarriorSpecBehaviour spec;

    List<ProjectileBehaviour> projectiles = new List<ProjectileBehaviour>();
    int projectileCounter = 0;

    float distance;
    List<float> attackSpeedCounters = new List<float>();
    bool fighting = false;

    float explosiveDamageOnDeath = 0f;
    float explosionRange = 5f;
    float timeToExplode = 0;
    bool exploded = false;

    public override Vector3 Location()
    {
        return transform.position;
    }

    public override void RecievedDamage(float damage)
    {
        if (exploded)
            return;
        base.RecievedDamage(damage);

        if (health <= 0)
        {
            if (explosiveDamageOnDeath > 0 && WarriorType().ExplosionOnDeath())
            {
                ExplosionAnimation();
            }
            else
            {
                Die();
            }
        }

        for (int i = 0; i < armPath.Count; i++)
        {
            if (spec.arms[i].Type() == GlobalAttributes.Type.Shield)
                animations[i].Play();
        }
    }

    public override float MaxHealth()
    {
        return Health() + spec.shield;
    }

    public override void Awake()
    {
        spec = GlobalAttributes.specs[Id()][GlobalAttributes.castleControllers[Id()].currentWarrior]; //Loading selected warrior type
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 1f;
        agent.speed *= (WarriorType().SpeedMultiplier() / spec.weight);

        base.Awake();
    }

    void Start()
    {
        BattleSceneManager.Instance().warriors[Id()].Add(this);
        LoadArmPaths();
        LoadArms();
        target = WarriorType().EnemyCastle();
    }

    void LoadArmPaths()
    {
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            if (t.name != "Canvas")
                armPath.Add(t);
        }
        animations = new Animation[armPath.Count];
    }

    void LoadArms()
    {

        for (int i = 0; i < armPath.Count; i++)
        {
            if (spec.arms[i].Object() != null)
            {
                animations[i] = (Instantiate(spec.arms[i].Object(), armPath[i], false).GetComponent<Animation>());
            }
            if (spec.arms[i].Type() == GlobalAttributes.Type.Weapon)
                attackSpeedCounters.Add(GetAttackCounter(spec.arms[i]));
            if (spec.arms[i].Type() == GlobalAttributes.Type.Explosive)
                explosiveDamageOnDeath += spec.arms[i].Damage() * WarriorType().DamageMultiplier();
            if (spec.arms[i].HasProjectile())
            {
                projectiles.Add(transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<ProjectileBehaviour>());
            }
        }
    }

    void Update()
    {
        CheckEnemy();
        FindEnemy();
        ChaseEnemy();
        Attack();
    }

    void CheckEnemy()
    {
        if (target == null) //Change target to enemy castle if there isnt any enemy on sight
        {
            for (int j = 0; j < attackSpeedCounters.Count; j++)
                attackSpeedCounters[j] = GetAttackCounter(spec.arms[j]);
            fighting = false;
            target = WarriorType().EnemyCastle();
        }
    }

    void FindEnemy()
    {
        if (spec.target != GlobalAttributes.Target.Castle && !fighting) //Find nearby enemies
        {
            float tempDistance;
            foreach (WarriorController warrior in BattleSceneManager.Instance().warriors[EnemyId()])
            {
                tempDistance = Vector3.Distance(transform.position, warrior.Location());
                if (tempDistance <= WarriorType().SightDistance() * spec.range)
                {
                    if (tempDistance < Vector3.Distance(transform.position, target.Location()))
                    {
                        target = warrior;
                    }
                }
            }
        }
        if (!fighting && (Vector3.Distance(transform.position, target.Location()) <= spec.range)) //Stop when u attacking
        {
            agent.isStopped = true;
            fighting = true;
        }
    }

    void ChaseEnemy()
    {
        if (tempTarget != target.transform.position) //Dont change destination if enemy doesnt move
        {
            tempTarget = target.transform.position;
            agent.SetDestination(target.Location());
        }
        distance = Vector3.Distance(transform.position, target.Location());
        if ((distance > spec.range && agent.isStopped) && !fighting) //Chase enemy if he runs away or move if enemy destroyed
        {
            agent.isStopped = false;
        }
    }

    void Attack()
    {
        if (fighting)
        {
            for (int i = 0; i < attackSpeedCounters.Count; i++)
            {
                if (spec.arms[i].Range() >= distance)
                {
                    attackSpeedCounters[i] -= Time.deltaTime;
                    if (attackSpeedCounters[i] <= 0)
                    {
                        if (spec.arms[i].HasProjectile())
                        {
                            projectiles[projectileCounter++ % projectiles.Count].Fire(target.transform.position, GetAttackCounter(spec.arms[i])); //Fire projectiles
                        }

                        attackSpeedCounters[i] = GetAttackCounter(spec.arms[i]);
                        target.RecievedDamage(spec.arms[i].Damage() * WarriorType().DamageMultiplier());
                        animations[i].Play();
                    }
                }
            }
            for (int i = 0; i < armPath.Count; i++)
            {
                if (spec.arms[i].Type() == GlobalAttributes.Type.Explosive && !exploded)
                {
                    if (spec.arms[i].Range() >= distance)
                    {
                        WarriorType().EnemyCastle().gold -= spec.cost / 2; //Self explosion doesnt give any reward to enemy
                        ExplosionAnimation();
                    }
                }
            }
        }
    }

    void Die()
    {
        WarriorType().EnemyCastle().gold += spec.cost / 2; //Enemy earns reward for killing warrior
        BattleSceneManager.Instance().warriors[Id()].Remove(this);
        foreach (ProjectileBehaviour p in projectiles)
            p.DestroyProjectiles();
        Destroy(gameObject);
    }

    void ExplosionAnimation()
    {
        exploded = true;
        for (int i = 0; i < armPath.Count; i++)
        {
            if (spec.arms[i].Type() == GlobalAttributes.Type.Explosive)
            {
                animations[i].Play();
                timeToExplode = animations[i].clip.length;
            }
        }
        Invoke("Explode", timeToExplode); //Explode after animation
    }

    void Explode()
    {
        foreach (WarriorController warrior in BattleSceneManager.Instance().warriors[EnemyId()].ToList()) //Explode and damage nearby enemies
        {
            if (warrior.health < 0)
                continue;
            if (Vector3.Distance(transform.position, warrior.Location()) < explosionRange)
                warrior.RecievedDamage(explosiveDamageOnDeath);
        }
        if (Vector3.Distance(transform.position, WarriorType().EnemyCastle().Location()) < explosionRange)
            WarriorType().EnemyCastle().RecievedDamage(explosiveDamageOnDeath);
        Die();
    }

    float GetAttackCounter(IArm arm) //Attackspeed timer
    {
        return 1f / (arm.AttackSpeed() * WarriorType().AttackSpeedMultiplier());
    }

    public abstract WarriorBehaviour WarriorType();
}