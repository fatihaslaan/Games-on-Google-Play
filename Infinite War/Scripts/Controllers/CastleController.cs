using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class CastleController : BaseController
{
    public float gold = 10;
    public int currentWarrior; //Spec index
    [HideInInspector]
    public float skillCost;

    ProjectileBehaviour projectile; //Castle can shoot to its enemy
    int enemyTmp = 0; //Number of enemy that castle can shoot (To determine using castle skill)
    float goldTimer = 2f; //Earning gold frequency
    float attackTimer = 2f; //Attackspeed
    BaseController enemy = null;
    Animation skillAnimation;

    protected Collider collider;

    public override void RecievedDamage(float damage)
    {
        base.RecievedDamage(damage);

        if (health <= 0)
        {
            BattleSceneManager.Instance().GameOver.SetActive(true); //Game over panel
            Time.timeScale = 0f; //Freeze time
            if (Id() != GlobalAttributes.selectedWarriorType) //Enemy castle lose the game
            {
                BattleSceneManager.Instance().GameOverText.GetComponent<Text>().text = "You Won 1 Star";
                GlobalAttributes.SaveDatas();
            }
        }
    }

    public override void Awake()
    {
        base.Awake();
        skillCost = 15f;
        projectile = transform.GetChild(0).GetComponent<ProjectileBehaviour>();
        skillAnimation = transform.GetChild(1).GetComponent<Animation>();
        collider = GetComponent<Collider>();
        GlobalAttributes.castleControllers[Id()] = this;
    }

    void Update()
    {
        FindEnemy();
        goldTimer -= Time.deltaTime;
        if (enemy)
            attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
        {
            projectile.Fire(enemy.Location(), 0f);
            enemy.RecievedDamage(ProjectileDamage());
            attackTimer = 2f;
        }
        if (goldTimer < 0)
        {
            gold++;
            goldTimer = 2f;
            if (Id() != GlobalAttributes.selectedWarriorType)
                Decision();
        }

    }

    void FindEnemy()
    {
        enemyTmp = 0;
        foreach (BaseController warrior in BattleSceneManager.Instance().warriors[EnemyId()])
        {
            if (Vector3.Distance(Location(), warrior.Location()) < 15f)
            {
                if (enemy == null)
                    enemy = warrior;
                enemyTmp++;
            }
        }
    }

    void Decision()
    {
        gold++;
        currentWarrior = Random.Range(0, 5);
        if (GlobalAttributes.specs[Id()][currentWarrior].cost <= gold)
        {
            if (BattleSceneManager.Instance().warriors[Id()].Count < GlobalAttributes.maxWarriorLimit[Id()])
            {
                SpawnWarrior();
                gold -= GlobalAttributes.specs[Id()][currentWarrior].cost;
            }
        }
        if (enemyTmp > 4)
        {
            if (enemyTmp > 6)
                gold += 7f; //Little help to ai if there is 7 enemy on sight of castle
            SkillAnimation();
        }
    }

    public void SkillAnimation()
    {
        if (gold < skillCost)
            return;
        gold -= skillCost;
        skillAnimation.Play();
        float time = skillAnimation.clip.length;
        Invoke("Skill", time);
    }

    void Skill()
    {
        foreach (BaseController warrior in BattleSceneManager.Instance().warriors[EnemyId()].ToList())
        {
            if (Vector3.Distance(Location(), warrior.Location()) < 19f)
            {
                warrior.RecievedDamage(SkillDamage());
            }
        }
    }

    public abstract float ProjectileDamage();

    public abstract float SkillDamage();

    public abstract void SpawnWarrior();

    public abstract GameObject Warrior();
}